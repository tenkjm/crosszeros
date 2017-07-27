#Представленный Api позволяет пользователям регистрироваться, создавать новые игры с другими пользователями, делать ходы по очереди, проверять игру на выигрыш. Две игры между одними и теми же пользователями создать нельзя, необходимо окончить текущую игру. Сам функционал ходов и проверки не тестировался из-за ограничения по времени. Для тестирования использовалась утилита Postman.

## Для создания нового пользователя использутеся Post запрос

http://localhost:5000/Auth/signup

с телом JSON, например,

{name: "anton@anton123.ru", 
email: "anton@anton123.ru", 
password: "Pa$$w0rd",
NickName: "Nicsfsdfsdttyttt"
}

##Для входа в систему POST

http://localhost:5000/Auth/login

с телом JSON, например,
{username: "anton@anton.ru", 
 
password: "Pa$$w0rd",
 
}

##Для выхода из системы  GET запрос
http://localhost:5000/Auth/logout

##Для просмотра списка назаконченных игр GET

http://localhost:5000/api/crosszeros/mygames

##Для создания новой игры GET
http://localhost:5000/api/crosszeros/newgame?oponentId=2bfc749c-58f5-484b-b4c5-87088d33f3bf

oponentId - необзодимо указать идентификатор пользователя

##Для просмотра списка доступных противников
http://localhost:5000/api/crosszeros/userlist


Для всех запросов на получение ответом является JSON с соответствующей информацией, например, для списка доступных противников JSON
 [
    {
        "userName": "anton@anton123.ru",
        "id": "2bfc749c-58f5-484b-b4c5-87088d33f3bf"
    },
    {
        "userName": "anton@anton.ru",
        "id": "c9f66093-f55b-4c34-b40d-ad1411cb8e6b"
    }
]


##Для проверки победы в игре 

использутеся запрос http://localhost:5000/api/crosszeros/chekwin?GameId=1

GameId - идентификатор игры



##Для выполнения хода

http://localhost:5000/api/crosszeros/move?row=1&column=1&GameId=1


##Для получения состояния игры

http://localhost:5000.api/crosszeros/gamestate?gameID=1

