/*
   sábado, 29 de junio de 201910:35:51
   Usuario: sa
   Servidor: EMCSERVERI7
   Base de datos: SHINE
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
CREATE TABLE dbo.[Client]
	(
	Id int NOT NULL IDENTITY (1, 1),
	Clientname varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	FechaRegistro datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.[Client] ADD CONSTRAINT
	PK_Client PRIMARY KEY CLUSTERED 
	(
	Clientname
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.[Client] SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.[Client]', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.[Client]', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.[Client]', 'Object', 'CONTROL') as Contr_Per 