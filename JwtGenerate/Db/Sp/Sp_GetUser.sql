USE [SHINE]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetUser]    Script Date: 30/06/2019 16:44:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetUser]
(
  @Username VARCHAR(50),
  @Email 	VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;

SELECT * FROM Client WHERE Username = @Username AND Email = @Email;

END
