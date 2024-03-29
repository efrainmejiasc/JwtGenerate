/*
   martes, 2 de julio de 201914:02:26
   User: sa
   Server: EFRAINMEJIASC
   Database: SHINE
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Client
	(
	Id int NOT NULL IDENTITY (1, 1),
	Name varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	Username varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	PhoneNumber varchar(50) NOT NULL,
	FavoriteGame varchar(50) NULL,
	BirthDate datetime NOT NULL,
	Gender varchar(50) NOT NULL,
	RegisteredDate datetime NOT NULL,
	RegisteredStatus bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Client SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Client ON
GO
IF EXISTS(SELECT * FROM dbo.Client)
	 EXEC('INSERT INTO dbo.Tmp_Client (Id, Name, LastName, Username, Password, Email, PhoneNumber, FavoriteGame, BirthDate, Gender, RegisteredDate, RegisteredStatus)
		SELECT Id, Name, LastName, Username, Password, Email, PhoneNumber, FavoriteGame, BirthDate, Gender, RegisteredDate, CONVERT(bit, RegisteredStatus) FROM dbo.Client WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Client OFF
GO
DROP TABLE dbo.Client
GO
EXECUTE sp_rename N'dbo.Tmp_Client', N'Client', 'OBJECT' 
GO
ALTER TABLE dbo.Client ADD CONSTRAINT
	PK_Client PRIMARY KEY CLUSTERED 
	(
	Email
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.Client', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Client', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Client', 'Object', 'CONTROL') as Contr_Per 