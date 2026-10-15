
CREATE PROCEDURE [dbo].[SPMOV_ListarRiesgosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

Select * from (
		-- ***** Escala Bieri ******
		SELECT '001' AS Codigo , CONVERT(int,VALORSEL) as Puntaje, 'Escala Bieri' AS Descripcion , [Common].[GETDATE]() as Fecha , Interpretacion = ''
		from HCESCABIE
		where IPCODPACI=@Paciente and NUMINGRES=@Ingreso

		-- ***** Escala Escala Apache ******
		UNION ALL
		select '002' as Codigo, (
		Case
			when PTSCREATI is null then 0
			else PTSCREATI
		END
		+
		Case
			when PTSEDAPAC is null then 0
			else PTSEDAPAC
		END
		+
		Case
			when PTSENFCRO is null then 0
			else PTSENFCRO
		END
		+
		Case
			when PTSFRECAR is null then 0
			else PTSFRECAR
		END
		+
		Case
			when PTSFRERES is null then 0
			else PTSFRERES
		END
		+
		Case
			when PTSGLAGLO is null then 0
			else PTSGLAGLO
		END
		+
		Case
			when PTSHEMATO is null then 0
			else PTSHEMATO
		END
		+
		Case
			when PTSLEUCOC is null then 0
			else PTSLEUCOC
		END
		+
		Case
			when PTSPHARTE is null then 0
			else PTSPHARTE
		END
		+
		Case
			when PTSPOTPLA is null then 0
			else PTSPOTPLA
		END
		+
		Case
			when PTSPREART is null then 0
			else PTSPREART
		END 
		+
		Case
			when PTSPREOXI is null then 0
			else PTSPREOXI
		END 
		+
		Case
			when PTSSODPLA is null then 0
			else PTSSODPLA
		END 
		+
		Case
			when PTSTEMPER is null then 0
			else PTSTEMPER
		END  ) as Puntaje, 'Escala Apache' as Descripcion , FECREGSIS as Fecha , Interpretacion = ''
		from HCESCAPAC
		where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
		UNION ALL
		SELECT '003' AS Codigo , (EDADPACIE	+	CAIDPREVI	+	TRANQUILI	+	DIURETICO	+	HIPOTENSO	+	ANTIPARKI	+	ANTIDEPRE	+	ALTERAVIS	+	ALTERAUDI	+	ICTUEXTRE	+	ESTADOMEN	+	SEGURAYUD	+	INSEGAYUD	+	IMPOSIBLE	+	PATOLOGIA	+	NUTRICION) as Puntaje, 'Escala DownTon' as Descripcion , FECREGSIS as Fecha, Interpretacion = ''
		from HCESCDOWN
		where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
		UNION ALL
		SELECT '004' AS Codigo , (ESTFISGEN +  ESTMENTAL + MOVILIDAD + ACTIVIDAD + INCTINENCI)  Puntaje, 'Escala NorTon' as Descripcion , FECREGSIS as Fecha , Interpretacion = ''
		from HCESCNTON
		where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
		UNION ALL
		SELECT '005' AS Codigo, CONVERT(int,PUNTAJESC) as Puntaje, 'Escala RASS' as Descripcion , FECREGSIS as Fecha , Interpretacion = ''
		from HCESCRASS
		where IPCODPACI=@Paciente and NUMINGRES=@Ingreso
		UNION ALL
		SELECT '006' AS Codigo, ( B.PTSESTREP + PTSESTMOV) as Puntaje, 'Escala VAS' as Descripcion , FECREGSIS as Fecha , Interpretacion = ''
		from HCESCVASC AS A
		inner join HCESCVASD B on A.CODCONSEC = B.CODCONSEC
		where A.IPCODPACI=@Paciente and A.NUMINGRES=@Ingreso
		) as P
group by Codigo, Puntaje, Descripcion, Fecha , Interpretacion
END