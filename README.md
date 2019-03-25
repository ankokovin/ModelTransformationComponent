# ModelTransformationComponent
Компонент трансформации моделей DSL

Для использования компонента требуется .Net Core версии 2.2.

## TransformationComponent
Библиотека компонента трансформации моделей DSL на .Net Core

## client
Консольный клиент данной библиотеки на .Net Core

Запуск: dotnet client.dll

### Аргументы

  client [-h|--h|-r|-fr|-f] <args>
  
  -h|--h : info
  
  -r : parse rules, <args> ::= <rules txt source><rules serialize target>
  
  -fr: parse model with already serialized rules, <args> ::= <model txt><rules serialized source><source language><target language><output txt>
  
  -f | none : parse both rules and model, <args> ::= <model txt><rules txt><source language><target language><output txt>


## Model Transformer Example
Приложение на Windows Forms, более наглядный ввод и вывод

## TransformationComponentUnitTest
Юнит тесты библиотеки компонента
