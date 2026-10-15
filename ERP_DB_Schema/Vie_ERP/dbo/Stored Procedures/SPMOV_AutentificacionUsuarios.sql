CREATE PROCEDURE [dbo].[SPMOV_AutentificacionUsuarios]
(
@Usuario Char(15),
@Password nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	Select  EstadoValidacion = 1,
		RTRIM(A.CODUSUARI) as Codigo,
		RTRIM(A.NOMUSUARI) as Nombre,
		RTRIM(A.USUEMAILE) as UserEmail,
		RTRIM(A.DESCARUSU) as Cargo,
	    case 
			when RTRIM(C.CODCENATE) is null then ''
			else RTRIM(C.CODCENATE)
		end as CodigoCentro,
		case 
			when RTRIM(C.NOMCENATE) is null then ''
			else RTRIM(C.NOMCENATE)
		end as NombreCentro,
		case 
			when RTRIM(B.UFUCODIGO) is null then ''
			else RTRIM(B.UFUCODIGO)
		end as CodigoUnidad,
		case 
			when RTRIM(B.UFUDESCRI) is null then ''
			else RTRIM(B.UFUDESCRI)
		end as NombreUnidad
	from SEGusuaru as A
	left join dbo.INUNIFUNC B on B.UFUCODIGO = A.UFUCODIGO
	left join dbo.ADCENATEN C on C.CODCENATE = A.CODCENATE
	WHERE A.CODUSUARI = @Usuario AND A.PASSUSUAR = @Password
end