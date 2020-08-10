CREATE DATABASE AppDb;

USE AppDb;

CREATE TABLE Usuarios
(
    Codigo int         not null primary key auto_increment,
    Nome   varchar(50) not null,
    Login  varchar(20) not null,
    Senha  varchar(16) not null
);

CREATE TABLE Clientes
(
    Codigo   int         not null primary key auto_increment,
    Nome     varchar(50) not null,
    Email    varchar(30) not null,
    Telefone varchar(15) not null
);