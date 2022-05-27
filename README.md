<h1 align="left">Для запуска</h1>
1 Клонировать репозиторий https://bitbucket.org/BUROVIC/osmlifesimulation.git
<br>
    &nbsp&nbsp&nbsp&nbsp1.1 Очистить кэш докера: docker builder prune
<br>
    &nbsp&nbsp&nbsp&nbsp1.2 Создать docker image: docker build --pull -t osmls
<br>
2 Получить библиотеку CityDataExpansionModule.dll
<br>
    &nbsp&nbsp&nbsp&nbspgit clone https://bitbucket.org/attapi343/citydataexpansionmodule source
<br>
    &nbsp&nbsp&nbsp&nbspdotnet publish ./source/CityDataExpansionModule -c release -o ./release
<br>
    &nbsp&nbsp&nbsp&nbspcp release/CityDataExpansionModule.dll .rm -rf source release
<br>
3 Запустить контейнер: docker run --rm -it -p 80:80 osmls
<br>
4 Перейти по http://localhost/index.html
<br>
5 Подключить (порядок важен)
<br>
    &nbsp&nbsp&nbsp&nbsp5.1 CityDataExpansionModule.dll
<br>
    &nbsp&nbsp&nbsp&nbsp5.1 dll текущего проекта