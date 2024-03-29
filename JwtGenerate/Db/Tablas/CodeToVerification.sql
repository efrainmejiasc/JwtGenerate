/*
   miércoles, 3 de julio de 201912:10:59
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
CREATE TABLE dbo.CodeToVerification
	(
	Id int NOT NULL IDENTITY (1, 1),
	Username varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	SendDate datetime NOT NULL,
	VerificationDate datetime NULL,
	Status bit NOT NULL,
	Code varchar(10) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CodeToVerification ADD CONSTRAINT
	PK_CodeToVerification PRIMARY KEY CLUSTERED 
	(
	Email
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.CodeToVerification SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CodeToVerification', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CodeToVerification', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CodeToVerification', 'Object', 'CONTROL') as Contr_Per 