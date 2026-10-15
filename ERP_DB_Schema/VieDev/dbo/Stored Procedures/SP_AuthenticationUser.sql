CREATE PROCEDURE [dbo].[SP_AuthenticationUser]
@NameUser char(60),
@PassUser char(50) AS
SELECT * FROM SEGusuaru WHERE NOMUSUARI = @NameUser AND PASSUSUAR = @PassUser