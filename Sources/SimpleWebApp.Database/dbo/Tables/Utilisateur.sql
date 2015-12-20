CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,  
	[Login] NVARCHAR(100) NOT NULL, 
	[PassswordHash] NVARCHAR(MAX)  NULL, 
	[Nom] NVARCHAR(256) NOT NULL, 
	[Email] NVARCHAR(256)  NULL, 
	[EmailConfirmed] BIT NOT  NULL, 
)
