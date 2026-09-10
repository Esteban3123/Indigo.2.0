CREATE VIEW [dbo].[ViewTherapy]
AS
	SELECT	A.CODSERIPS,
			CAST(0 AS BIT) AS Seleccione, 
			a.IPCODPACI, 
			NUMINGRES,
			'' as Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			COUNT(A.CODSERIPS) AS TOTAL,
			i.IPNOMCOMP  as PersonName,
			B.ARSCODIGO,
			A.CODPROSAL,
			CODESPEC1,
			TIPSERIPS
	FROM dbo.HCPROCTER AS A 
	JOIN dbo.INCUPSIPS AS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO 
	JOIN dbo.INPROFSAL D ON A.CODPROSAL=D.CODPROSAL 
	JOIN dbo.INESPECIA E ON D.CODESPEC1=E.CODESPECI 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	WHERE TIPSERTER='1'
	GROUP BY A.CODSERIPS,B.DESSERIPS,B.ARSCODIGO,A.CODPROSAL,CODESPEC1,TIPSERIPS,a.IPCODPACI,NUMINGRES,i.IPNOMCOMP
UNION ALL
	SELECT	A.CODSERIPS,
			CAST(0 AS BIT) AS Seleccione, 
			a.IPCODPACI, 
			INGMH.NUMINGRES,
			'' as Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			COUNT(A.CODSERIPS) AS TOTAL,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
			B.ARSCODIGO,
			A.CODPROSAL,
			CODESPEC1,
			TIPSERIPS
	FROM dbo.HCPROCTER AS A 
	JOIN dbo.INCUPSIPS AS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INUNIFUNC C ON A.UFUCODIGO=C.UFUCODIGO 
	JOIN dbo.INPROFSAL D ON A.CODPROSAL=D.CODPROSAL 
	JOIN dbo.INESPECIA E ON D.CODESPEC1=E.CODESPECI 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	WHERE TIPSERTER='1'
	GROUP BY A.CODSERIPS,B.DESSERIPS,B.ARSCODIGO,A.CODPROSAL,CODESPEC1,TIPSERIPS,a.IPCODPACI,INGMH.NUMINGRES,rn.NUMHIJREG
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los servicios de terapia (procedimientos con tipo de servicio de terapia, TIPSERTER=''1'') realizados a pacientes, mostrando el código y descripción del servicio CUPS, el número de ingreso, el nombre del paciente y el profesional de salud responsable con su especialidad. Combina dos grupos de registros: los procedimientos asociados directamente al ingreso del paciente adulto o regular, y los procedimientos registrados bajo el ingreso de recién nacidos vinculados a la madre, identificando al hijo por su número de registro. Se usa para reportería y gestión de terapias aplicadas durante hospitalizaciones, permitiendo visualizar tanto pacientes generales como neonatos dentro del mismo resultado, agrupando el total de veces que se realizó cada servicio por ingreso y profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewTherapy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewTherapy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar el listado agregado de procedimientos terapéuticos por servicio para cada paciente/ingreso, incluyendo tanto al paciente titular como a los recién nacidos asociados al ingreso de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de los catálogos relacionados (INCUPSIPS, INUNIFUNC, INPROFSAL, INESPECIA) con las claves referenciadas en HCPROCTER.; En la segunda rama, debe existir vínculo madre-hijo en HCINGRESORECNAC y registro en HCRECINAC y ADINGRESO con NUMINGRESHIJO coincidente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen procedimientos cuyo tipo de servicio terapéutico es ''1'' (TIPSERTER=''1'').; Se requiere que cada procedimiento tenga unidad funcional, profesional, especialidad y servicio CUPS válidos (joins internos).; El conteo TOTAL agrupa procedimientos por servicio, paciente, ingreso y profesional/especialidad.; La vista combina dos universos disjuntos por construcción: procedimientos del paciente titular y procedimientos asociados a ingresos de recién nacidos vinculados a la madre.; El campo Seleccione siempre se inicializa en 0 (false) y Tipo siempre vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimientos terapéuticos; Pacientes; Ingreso/Admisión hospitalaria; Profesional de salud; Especialidad médica; Unidad funcional; Recién nacido; Servicios CUPS; ARS / Aseguradora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas agrupadas (COUNT por CODSERIPS) filtrando únicamente procedimientos con TIPSERTER=''1'', uniendo dos conjuntos: procedimientos del paciente y procedimientos vinculados a ingreso de recién nacido a través de HCINGRESORECNAC/HCRECINAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El servicio del procedimiento corresponde al ingreso del paciente titular → Se reporta el nombre completo del paciente desde INPACIENT como PersonName else Si el ingreso corresponde a un recién nacido (vinculado vía HCINGRESORECNAC), se reporta ''Hijo '' + número de hijo del registro de recién nacido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPROCTER; dbo.INCUPSIPS; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewTherapy';
GO
