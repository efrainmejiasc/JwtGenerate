USE [SHINE]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertUser]    Script Date: 29/06/2019 10:52:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetUser]
(
  @Username VARCHAR(50),
  @Password  	VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT * FROM Client WHERE Username = @Username AND Password = @Password;

END
