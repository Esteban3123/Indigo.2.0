
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consolida y presenta todas las escalas de valoración clínica y riesgo registradas para un paciente en un ingreso específico. Recibe como parámetros la cédula del paciente y el número de ingreso, y devuelve en una sola consulta el puntaje calculado de seis escalas clínicas: Bieri (dolor), Apache (gravedad en cuidados intensivos), DownTon (riesgo de caídas), Norton (riesgo de úlceras por presión), RASS (sedación y agitación) y VAS (escala visual analógica de dolor/estrés). Para cada escala retorna un código identificador, el puntaje total obtenido sumando los ítems evaluados, el nombre de la escala y la fecha de registro. Es utilizado por la interfaz clínica para mostrar al profesional de la salud el resumen de riesgos del paciente durante su hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarRiesgosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los puntajes de las distintas escalas de riesgo clínico aplicadas a un paciente durante un ingreso hospitalario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente y el número de ingreso en las tablas de escalas para que devuelva filas.; Para la escala VAS se requiere correspondencia entre cabecera y detalle por CODCONSEC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada escala se identifica con un código fijo: 001 Bieri, 002 Apache, 003 Downton, 004 Norton, 005 RASS, 006 VAS.; El puntaje de Apache se calcula como suma de 14 componentes tratando NULL como 0.; El puntaje de Downton se obtiene sumando 16 factores de riesgo de caída.; El puntaje de Norton suma estado físico, mental, movilidad, actividad e incontinencia.; El puntaje de VAS suma la valoración en reposo y en movimiento.; La interpretación siempre se devuelve como cadena vacía.; El campo Fecha de la escala Bieri se obtiene de la fecha actual del sistema vía [Common].[GETDATE](), no de la tabla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Escala de dolor Bieri; Escala Apache; Escala Downton (riesgo de caídas); Escala Norton (riesgo de úlceras por presión); Escala RASS (sedación-agitación); Escala VAS (dolor visual analógica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada escala registrada para el paciente e ingreso, con código, puntaje calculado, descripción de la escala, fecha e interpretación vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Para la escala Apache, cada componente de puntaje (creatinina, edad, enfermedad crónica, frecuencia cardíaca/respiratoria, glóbulos, hematocrito, leucocitos, pH, potasio, presión arterial/oxígeno, sodio, temperatura) es NULL → Se sustituye por 0 antes de sumar else Se usa el valor registrado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCESCABIE; dbo.HCESCAPAC; dbo.HCESCDOWN; dbo.HCESCNTON; dbo.HCESCRASS; dbo.HCESCVASC; dbo.HCESCVASD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarRiesgosPacientes';
-- GO
