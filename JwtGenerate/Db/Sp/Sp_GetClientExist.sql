USE [SHINE]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetClientExist]    Script Date: 02/07/2019 17:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetClientExist]
(
  @Password VARCHAR(50),
  @Email 	VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;
       DECLARE @Resultado INT;

                 SET @Resultado = (SELECT ID FROM Client WHERE Password = @Password AND Email = @Email);		
				 SELECT @Resultado;
END
