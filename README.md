# UnityJuniorMiddleDeveloperTestJob
https://github.com/Andrey187/UnityJuniorMiddleDeveloperTestJob/assets/114676059/cd13cb99-25d1-4759-9fa0-0ac4064447f2


# Описание проекта

Этот проект представляет собой простую сцену, где реализована атака двух разных типов башен по монстрам. В этом файле я предоставляю пояснения к архитектурным решениям и настройкам проекта.

## Архитектурные решения

Проект был разработан с использованием Script and GameObject Architecture и паттернов для обеспечения модульности и расширяемости.

### Пул объектов

Для оптимизации производительности реализован паттерн Object Pooling для монстров и снарядов. Это позволяет избегать частых созданий и удалений объектов во время игры.

### Разделение поведения и классов

Поведение монстров и снарядов разделено с использованием интерфейсов. Это делает код более гибким и позволяет легко добавлять новые типы монстров и снарядов в будущем.

### Типы снарядов

Каждая башня имеет свой тип снарядов. Это позволяет создавать легко новые классы снарядов.

### Атака

Атака разделена на абстрактный класс, а также на конкретные классы для каждого типа башен.
Так же режимы стрельбы были реализованы через состояния, что позволяет лучше настраивать и интегрировать в будущем разные режимы стрельбы для разных типов башен.

### Поворот башни

Поворот башни был реализован через методы, управляемые булевой переменной. 

## Настройки проекта

В данной сцене присутствуют две разные башни и монстр, которые были настроены следующим образом:

-  Для оптимизации работы проекта были приняты следующие решения: 
1. Убраны коллайдеры с башен. 
2. Убраны с некоторых  частей башен настройки освещения и теней, т.к на сцене этого не видно. 
3. Статичные части башен помечены галочкой Static, что способствует работе статичного батчинга.
4. Тени у снарядов и монстров были оставлены, хоть это и не способствует работе динамичного батчинга, т.к нет конкретной задачи. 
- Для удобной смены режима стрельбы, была добавлена простая кнопка.
