using ModelTransformationComponent.SystemRules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ModelTransformationComponent
{
    public partial class DependencyGraph
    {
        private static System.Text.RegularExpressions.Regex Important = new System.Text.RegularExpressions.Regex(@"[\w\d]");
        Node rootNode;
        Dictionary<string, Node> sourceNodes;
        Dictionary<string, Node> targetNodes;

        public DependencyGraph(in AllRules allRules, string sourceLang, string targetLang, string root="Program")
        {
            sourceNodes = new Dictionary<string, Node>();
            targetNodes = new Dictionary<string, Node>();


            var slang = allRules.GetRulesForLanguage(sourceLang);
            Dictionary<string, Rule> cslang = new Dictionary<string, Rule>();

            foreach (var item in slang)
            {
                if (!item.Key.StartsWith("T+"))
                    cslang.Add(item.Key, item.Value);
            }

            var sblang = allRules.GetBaseRules;
            Dictionary<string, Rule> csblang = new Dictionary<string, Rule>();

            foreach (var item in sblang)
            {
                if (!item.Key.StartsWith("T+"))
                    csblang.Add(item.Key, item.Value);
            }


            BuildTree(csblang, ref sourceNodes, true);

            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w/o base");
                foreach (var item in sourceNodes)
                {
                    Debug.WriteLine(item);
                }
            }

            BuildTree(cslang, ref sourceNodes, false);


            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w base");
                foreach (var item in sourceNodes)
                {
                    Debug.WriteLine(item);
                }
            }

            foreach (var item in sourceNodes)
            {
                if (item.Value.rule is BNFRule b)
                    HandleBNFLink(ref sourceNodes, b);
            }
            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w links");
                foreach (var item in sourceNodes)
                {
                    Debug.WriteLine(item);
                }
            }


            rootNode = sourceNodes[root];




            var tlang = allRules.GetRulesForLanguage(targetLang);

            Dictionary<string, Rule> ctlang = new Dictionary<string, Rule>();
            foreach (var item in tlang)
            {
                if (item.Key.StartsWith("T+"))
                {
                    NamedRule namedRule = (NamedRule)item.Value;
                    namedRule.Name = item.Key.Substring(2);
                    ctlang[namedRule.Name] = namedRule;
                } else if (!ctlang.ContainsKey(item.Key))
                {
                    ctlang.Add(item.Key, item.Value);
                }
            }



            var blang = allRules.GetBaseRules;
            Dictionary<string, Rule> cblang = new Dictionary<string, Rule>();
            foreach (var item in blang)
            {
                if (!ctlang.ContainsKey(item.Key))
                {
                    if (item.Key.StartsWith("T+"))
                    {
                        NamedRule namedRule = (NamedRule)item.Value;
                        namedRule.Name = item.Key.Substring(2);
                        cblang[namedRule.Name] = namedRule;
                    }
                    else
                    {

                        ctlang.Add(item.Key, item.Value);

                    }
                }
            }



            BuildTree(cblang, ref targetNodes, true);


            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w/o base");
                foreach (var item in targetNodes)
                {
                    Debug.WriteLine(item);
                }
            }
            BuildTree(ctlang, ref targetNodes, false);

            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w base");
                foreach (var item in targetNodes)
                {
                    Debug.WriteLine(item);
                }
            }


            foreach (var item in targetNodes)
            {
                if (item.Value.rule is BNFRule b)
                    HandleBNFLink(ref targetNodes, b);
            }
            if (Debugger.IsAttached)
            {
                Debug.WriteLine("Tree w links");
                foreach (var item in targetNodes)
                {
                    Debug.WriteLine(item);
                }
            }
        }

        public List<BNode> buildTree(in string text)
        {
            List<BNode> result = new List<BNode>();
            int idx = 0;
            depthCounter = 0;
            while (idx < text.Length)
            {
                var temp = tryRule(in text, idx, rootNode, out int charCount);
                if (temp == null)
                    throw new TransformComponentException();
                result.Add(temp);
                idx += charCount;
            }
            return result;
        }
        private BNode tryRule(in string text, int startIdx, Node node, out int charCount)
        {
            Debug.WriteLine("tryRule startIdx:{0}, node" + node, startIdx);
            switch (node.rule)
            {
                case BNFRule rule:
                    return tryBnf(text, startIdx, rule, out charCount);
                case RegexRule rule:
                    return tryRegex(text, startIdx, rule, out charCount);
                case SystemRule rule:
                    return trySystem(text, startIdx, rule, out charCount);
                default:
                    throw new Exception("WTF");
            }
        }
        const int maxDepthCounter = 100;
        int depthCounter;
        private BNode tryBnf(in string text, int startIdx, BNFRule rule, out int charCount)
        {
            if (++depthCounter > maxDepthCounter)
            {
                Debug.WriteLine("Overflow happend");
                charCount = -1;
                return null;
            }
            Debug.WriteLine("tryBnf startIdx:{0}, rule" + rule, startIdx);
            charCount = -1;
            BNode result = null;

                for (int i = 0; i < rule.Count; i++)
                {
                    var temp = trySimpleBNF(text, startIdx, rule[i], out int t, rule.Name);
                    if (t > charCount && temp != null)
                    {
                        result = temp;
                        charCount = t;
                        result.Value = i.ToString();
                        result.isRef = true;
                    }
                }
            

            --depthCounter;
            return result;
        }
        

        private BNode trySimpleBNF(in string text, int startIdx, BasicBNFRule rule, out int charCount, string name)
        {
            Debug.WriteLine("trySimpleBNF startIdx:{0}, rule" + rule, startIdx);
            charCount = -1;
            BNode result = new BNode(name);
            int t = 0;
            int offset = 0;
            foreach (var item in rule)
            {
                if (name == "Program" || name == "Program.program_body" || name == "program_body")
                {
                    Debug.WriteLine("Pog!?");
                }
                BNode temp = null;
                switch (item)
                {
                    case BNFReference refr:
                        temp = tryRule(text, startIdx+ offset, sourceNodes[refr.Name] , out t);
                        break;
                    case BNFSystemRef sref:
                        temp = trySystem(text, startIdx+ offset, sref.rule, out t);
                        break;
                    case BNFString bNFString:
                        temp = tryString(text, startIdx+ offset, bNFString.Value, out t);
                        break;
                    default:
                        throw new TransformComponentException();
                }
                offset += t;
                if (temp == null)
                    return null;
                result.Children.Add(temp);
                temp.Parrent = result;
            }
            charCount = offset;
            return result;
        }
        
        private BNode tryString(in string text, int startIdx, string str, out int charCount)
        {
            Debug.WriteLine("trySimpleBNF startIdx:{0}, str" + str, startIdx);
            charCount = -1;
            if (text.Substring(startIdx).StartsWith(str))
            {
                charCount = str.Length;
                return new BNode("str") { Value = str, isRef=false };
            }
            return null;
        }

        private BNode tryRegex(in string text, int startIdx, RegexRule rule, out int charCount)
        {
            Debug.WriteLine("tryRegex startIdx:{0}, regex" + rule, startIdx);
            charCount = -1;
            var search = rule.regex.Match(text.Substring(startIdx));
            if (search.Success)
            {
                charCount = search.Length;
                BNode bNode;
                if (Important.IsMatch(search.Value))
                    bNode = new BNode(rule.Name)
                    {
                        Value = search.Value,
                        isRef = false
                    };
                else
                    bNode = new BNode("Blocked")
                    {
                        Value = string.Empty,
                        isRef = false
                    };
                return bNode;
            }
            return null;
        }

        private BNode trySystem(in string text, int startIdx, SystemRule systemRule, out int charCount)
        {
            Debug.WriteLine("trySystem startIdx:{0}, rule:" + systemRule, startIdx);
            charCount = -1;
            switch (systemRule)
            {
                case Space space:
                    if (text[startIdx] == ' ')
                    {
                        charCount = 1;
                        var res = new BNode("space");
                        return res;
                    }
                    break;
                case Empty empty:
                    charCount = 0;
                    return new BNode("empty");
                case New_line new_Line:
                    if (text[startIdx] =='\r' && text[startIdx+1] == '\n')
                    {
                        charCount = 2;
                        return new BNode("new line");
                    }
                    if (text[startIdx] == '\n')
                    {
                        charCount = 1;
                        return new BNode("new line");
                    }
                    break;
                default:
                    break;
            }
            return null;
        }

        public string toText(List<BNode> tree)
        {
            GeneratorState generatorState = new GeneratorState();
            foreach (var item in tree)
            {
                generatorState =  toText(item, generatorState);
            }
            return generatorState.Text;
        }

        private GeneratorState toText(BNode node, GeneratorState generatorState)
        {
            if (node.RuleName == "str") throw new TransformComponentException();

            switch (targetNodes[node.RuleName].rule)
            {
                case BNFRule bNFs:
                    if (node.isRef && int.TryParse(node.Value, out int n))
                        generatorState = toText(node, bNFs[n], generatorState);
                    else
                        generatorState.AppendText(node.Value);
                    
                    break;
                case RegexRule regexRule:
                    generatorState.AppendText(node.Value);
                    break;
            }
            return generatorState;
        }
        
        private GeneratorState toText(BNode node, BasicBNFRule basic , GeneratorState generatorState)
        {
            foreach (var item in basic)
            {
                generatorState = toText(node, item, generatorState);
            }
            return generatorState;
        }

        private GeneratorState toText(Rule rule, GeneratorState generatorState)
        {
            switch (rule)
            {
                case RegexRule reg:
                    return toText(reg, generatorState);
                case BNFRule bnfr:
                    return toText(bnfr, generatorState);
                case SystemRule systemRule:
                    return toText(systemRule, generatorState);
            }
            return generatorState;
        }

        private GeneratorState toText(RegexRule rule, GeneratorState generatorState)
        {
            if (rule.Banned) return generatorState;
            generatorState.AppendText(rule.Pattern.Substring(1));
            return generatorState;
        }

        private GeneratorState toText(BNFRule bnfr, GeneratorState generatorState)
        {
            var item = bnfr[0];
            foreach (var item2 in item)
            {
                if (item2 is BNFString str)
                    generatorState.AppendText((str).Value);
                if (item2 is BNFReference bNFReference)
                {
                    generatorState = toText(targetNodes[bNFReference.Name].rule, generatorState);
                }
                if (item2 is BNFSystemRef sref)
                    generatorState = toText(sref.rule, generatorState);
            }
            return generatorState;
                
        }

        private GeneratorState toText(SystemRule systemRule, GeneratorState generatorState)
        {
            if (systemRule is IChangeState c)
                c.ChangeState(ref generatorState);
            return generatorState;
        }

        private GeneratorState toText(BNode node, BNFSimpleElement element, GeneratorState generatorState)
        {
            switch (element)
            {
                case BNFString bNFString:
                    generatorState.AppendText(bNFString.Value);
                    return generatorState;
                case BNFReference bNFReference:
                    var child = node.Children.Find(x => x.RuleName == bNFReference.Name);
                    if (child == null)
                        return toText(targetNodes[bNFReference.Name].rule, generatorState);
                    return toText(child, generatorState);
                case BNFSystemRef bNFSystemRef:
                    if (bNFSystemRef.rule is IChangeState s)
                    {
                        s.ChangeState(ref generatorState);
                        return generatorState;
                    }
                    break;
                default:
                    break;
            }
            throw new TransformComponentException("WHO DID THIS???" + element.GetType()+":"+ element.ToString());
        }


        public string TransformText(string text)
        {
            var tree = buildTree(text);
            return toText(tree);
        }

        private void BuildTree(in Dictionary<string, Rule> rules, ref Dictionary<string, Node> nodes, bool isBase)
        {
            foreach(var Rule in rules)
            {
                if (!nodes.ContainsKey(Rule.Key) || !isBase && nodes[Rule.Key].fromBase)
                {
                    switch (Rule.Value)
                    {
                        case BNFRule rule:
                            HandleBNFRule(in rules,ref nodes, rule, isBase);
                            break;
                        case RegexRule reg:
                            handleRegex(in rules, ref nodes, reg, isBase);
                            break;
                        case SystemRule sys:
                            throw new Exception("WTF");
                    }
                }
            }
        }


        private string HandleBNFRule(in Dictionary<string, Rule> rules, ref Dictionary<string, Node> nodes,  BNFRule bNF, bool isBase)
        {
            Debug.WriteLine(bNF.ToString());
            if (isBase || !nodes.ContainsKey(bNF.Name))
            {

                Node n = new Node(bNF, isBase);
                nodes[bNF.Name] = n;
            }
            else
            {
                nodes[bNF.Name].rule = bNF;
                nodes[bNF.Name].fromBase = isBase;
            }
            return bNF.Name;
        }


        private string handleRegex(in Dictionary<string, Rule> rules, ref Dictionary<string, Node> nodes, RegexRule regexRule, bool isBase)
        {
            if (!nodes.ContainsKey(regexRule.Name))
            {
                Node n = new Node(regexRule, isBase);
                nodes[regexRule.Name] = n;
            }
            return regexRule.Name;
        }
        


        private void HandleBNFLink(ref Dictionary<string, Node> nodes,  BNFRule bNF)
        {
            foreach (var basic in bNF)
            {
                foreach (var item in basic)
                {
                    if (item is BNFReference reference && !nodes[bNF.Name].Children.Where(x=> x.rule is NamedRule n && n.Name == (reference.Name) ).Any())
                    {
                        nodes[bNF.Name].Children.Add(nodes[reference.Name]);
                        nodes[reference.Name].Parent.Add(nodes[bNF.Name]);
                    }
                }
            }
        }
    }
}
