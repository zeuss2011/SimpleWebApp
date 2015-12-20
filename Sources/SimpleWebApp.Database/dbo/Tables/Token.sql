CREATE TABLE [dbo].[Token]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UtilisateurId] INT NOT NULL, 
	[AuthToken] NVARCHAR(500) NOT NULL, 
	[DateCreation] DATETIME NOT NULL, 
	[DateExpiration] DATETIME NOT NULL, 
    CONSTRAINT [FK_Token_Utilisateur] FOREIGN KEY ([UtilisateurId]) REFERENCES [Utilisateur]([Id])
)
