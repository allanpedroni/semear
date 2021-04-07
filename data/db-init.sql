Print 'Criando banco DbAnuncio...'

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DbAnuncio')
    BEGIN
        CREATE DATABASE DbAnuncio
    END
GO

USE [DbAnuncio]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Anuncio')
    BEGIN
        CREATE TABLE [dbo].[Anuncio]
        (
            [Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
            [Texto] VARCHAR(1000) NOT NULL, 
            [Imagem] VARCHAR(MAX) NOT NULL, 
            [Latitude] DECIMAL(12,9) NOT NULL, 
            [Longitude] DECIMAL(12,9) NOT NULL
        )
    END
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'AnuncioAcesso')
    BEGIN
        CREATE TABLE [dbo].[AnuncioAcesso]
        (
            [Id] INT NOT NULL,
            [TotalAcesso] INT NOT NULL, 
            FOREIGN KEY (Id) REFERENCES dbo.Anuncio (Id)
        )
    END
GO 

DECLARE @imagem AS VARCHAR(MAX)='/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCAcHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAfAB8DASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwDzr9lfXPFGoXWn+KNG02FNT0tRexRRblW4lREni8r+JXb733vm+b5vlbb9r/GT/grf4n1z4SeGvEdtcXPhzVr6xeC3sLKXykbb8jyzL/vKrKPl2ttT7rPu+APjJ4T1z4O3Gk6Zq2pNpskmqRXVqbfdBbXjNFF50DKzvFE6yyo6r8zfumVWZlWuH/4aA1nx7rVsmuWFxqc94m2zsyy/abO0i3M3mlfuS7pWfay7vl+ZPmTd4HDueUfavERpqpRktNdVZ3ur6PZvpbVavbixmHnKHs4ycX+Z6p44+MmseKNavL/U7y8vb66bzbq4uZWllmb+87N8zV5xrHxIe4mbD5/4FWh/wjseuX1/Z2dnrxmt2R/K2bnk3uqsju21dqMrfMu35VZm21y2p+HbXwzYs8q3CzZ+RbhymWba3305HyNnA/vYP3a/RP8AXLL5aQbv2PH/ALNrR1Z9e/tMeE9P+Klnp+neRpo021uIlaS40u3nlk3J9xGdGZdqvL8y7dv8LLubd4zov7F9tqHxKi8Sw6VI+oLPK9xEZHl8uVl+VlZ2ZmX+L5mr7J8XR23w08JJqVyLgyo6yJbhkBkLFUwfldepHfpXS6zJd6J8MtF12SCK8j1GIOCqpG65DMN2cj/vkd64MsyTI8spfVYU0opPXXrvr1P5ulxjxlmsfreFnK19k9F/l9+p893HgnW1tUtJtN8+w01dtvEks6tud9zr8r7W+9/Eu3ctc83wm12TXpNVsrCNNRk5f9yHd8qoPyspAHy5wR/F+Nehaz+0V5HiptG1LSdQ07yAGnuYbe1uooSW2oQpkV/MMnXGVw33ht+bmpdG0rx3d3MWi+O7uPWIZ2+1Ws9s8DW/JAKP5MyANhsgHPy9ea56dLhhXeHhGXzX63O95vx0mo4urKHXZy0/7d6f11P/2Q=='

INSERT INTO Anuncio (Texto, Imagem, Latitude, Longitude) VALUES ('Texto do Anuncio AA',@imagem, -19.971596897022884, -43.959746963082964)
INSERT INTO AnuncioAcesso(Id, TotalAcesso) VALUES(SCOPE_IDENTITY(), 0)
INSERT INTO Anuncio (Texto, Imagem, Latitude, Longitude) VALUES ('Texto do Anuncio AB',@imagem, -19.971596897022884, -43.959746963082964)
INSERT INTO AnuncioAcesso(Id, TotalAcesso) VALUES(SCOPE_IDENTITY(), 0)
INSERT INTO Anuncio (Texto, Imagem, Latitude, Longitude) VALUES ('Texto do Anuncio BA',@imagem, -19.969029681027706, -43.96079042234345)
INSERT INTO AnuncioAcesso(Id, TotalAcesso) VALUES(SCOPE_IDENTITY(), 0)
INSERT INTO Anuncio (Texto, Imagem, Latitude, Longitude) VALUES ('Texto do Anuncio CA',@imagem, -19.969775879713456, -43.96447041281299)
INSERT INTO AnuncioAcesso(Id, TotalAcesso) VALUES(SCOPE_IDENTITY(), 0)
GO

Print 'Fim criação banco DbAnuncio...'




Print 'Criando banco DbUsuario...'

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DbUsuario')
    BEGIN
        CREATE DATABASE DbUsuario
    END
GO

USE [DbUsuario]
GO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Usuario')
    BEGIN
        CREATE TABLE [dbo].[Usuario]
        (
            [Id] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
            [Nome] VARCHAR(500) NOT NULL, 
            [Email] VARCHAR(500) NOT NULL, 
            [Senha] VARCHAR(500) NOT NULL, 
            [Ativo] BIT NOT NULL
        )
    END
GO 

INSERT INTO Usuario (Nome, Email, Senha, Ativo) VALUES ('Allan Barros', 'allan.barros@gmail.com', '123456', 0)
GO

Print 'Fim criação banco DbAnuncio...'