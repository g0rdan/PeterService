## Disclaimer

- У нас был разговор по поводу сервисов и мы, возможно, друг друга не поняли. Мое привычное понимание сервисов это то, что предоставлеят какие-либо "услуги". Это совсем не обязательно то, что должно крутится где-то в background, но второе конечно может быть сервисом. В общем, в коде и так все описано, посмотри.

- Так же мне показалось друг друга не поняли в плане что такое ViewModel и с чем это едят. Вроде тоже довольно точно к коде показал, что VM это "представление" view и что когда есть VM со всей бизнес логикой, то например unit тесты в таком случае покрывают все user cases (это к слову, почему я не пишу UI тесты). К слову, у меня не получилось завести UI тесты под iOS, но написано несколько под Android и это надо сказать интересный опыт) Мне определено нужно уделить больше внимания UI тестам в Xamarin.

- На самом деле я хотел начать проект в связке с MVP, потому как последнее время ме интересна Clean архитектура (думал что-то попробовать от туда) и она хорошо ложится поверх MVP, как я понимаю. Но недостаток опыта испугал. Плюс в этом тестовом задании используется MvvmCross, и если он используется, то на него завязывается очень много аспектов работы приложения, что не очень хорошо и что последнее время довольно сильно бесит.