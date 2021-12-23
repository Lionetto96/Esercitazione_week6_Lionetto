create database GestioneSpesa
create table Categoria(
IdCategory int primary key identity(1,1),
Nome varchar(20))

create table Spesa(
Id int identity(1,1) primary key,
Data date,
Descrizione varchar(50),
Utente varchar(20),
Importo decimal(18,2),
Approvata bit,
IdCategory int,
constraint FK_1 foreign key(IdCategory) references Categoria(IdCategory))

insert into Categoria values ('bollette')
insert into Categoria values ('affitto')
select * from Categoria
