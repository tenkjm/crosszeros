#�������������� Api ��������� ������������� ����������������, ��������� ����� ���� � ������� ��������������, ������ ���� �� �������, ��������� ���� �� �������. ��� ���� ����� ������ � ���� �� �������������� ������� ������, ���������� �������� ������� ����. ��� ���������� ����� � �������� �� ������������ ��-�� ����������� �� �������. ��� ������������ �������������� ������� Postman.

## ��� �������� ������ ������������ ������������ Post ������

http://localhost:5000/Auth/signup

� ����� JSON, ��������,

{name: "anton@anton123.ru", 
email: "anton@anton123.ru", 
password: "Pa$$w0rd",
NickName: "Nicsfsdfsdttyttt"
}

##��� ����� � ������� POST

http://localhost:5000/Auth/login

� ����� JSON, ��������,
{username: "anton@anton.ru", 
 
password: "Pa$$w0rd",
 
}

##��� ������ �� �������  GET ������
http://localhost:5000/Auth/logout

##��� ��������� ������ ������������� ��� GET

http://localhost:5000/api/crosszeros/mygames

##��� �������� ����� ���� GET
http://localhost:5000/api/crosszeros/newgame?oponentId=2bfc749c-58f5-484b-b4c5-87088d33f3bf

oponentId - ���������� ������� ������������� ������������

##��� ��������� ������ ��������� �����������
http://localhost:5000/api/crosszeros/userlist


��� ���� �������� �� ��������� ������� �������� JSON � ��������������� �����������, ��������, ��� ������ ��������� ����������� JSON
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


##��� �������� ������ � ���� 

������������ ������ http://localhost:5000/api/crosszeros/chekwin?GameId=1

GameId - ������������� ����



##��� ���������� ����

http://localhost:5000/api/crosszeros/move?row=1&column=1&GameId=1


##��� ��������� ��������� ����

http://localhost:5000.api/crosszeros/gamestate?gameID=1

