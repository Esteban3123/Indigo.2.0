-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 14/05/2019,>  
-- Description: <Description, HC de Primera Infancia, Infancia y adoslecencia>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia] @FechaIni DATETIME, 
                                                                                    @FechaFin DATETIME
AS
    BEGIN
        SELECT CASE B.IPTIPODOC
                   WHEN '1'
                   THEN 'CC: Cédula de Ciudadanía'
                   WHEN '2'
                   THEN 'CE: Cédula de Extranjería'
                   WHEN '3'
                   THEN 'TI: Tarjeta de Identidad'
                   WHEN '4'
                   THEN 'RC: Registro Civil'
                   WHEN '5'
                   THEN 'PA: Pasaporte'
                   WHEN '6'
                   THEN 'AS: Adulto Sin Identificación'
                   WHEN '7'
                   THEN 'MS: Menor Sin Identificación'
                   WHEN '8'
                   THEN 'NU: Número único de identificación personal'
                   WHEN '9'
                   THEN 'CN: Certificado Nacido Vivo'
                   WHEN '10'
                   THEN 'CD: Carnet Diplomático'
                   WHEN '11'
                   THEN 'SC: Salvoconducto'
                   ELSE 'PE: Permiso especial de Permanencia'
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               C.UBINOMBRE 'Ubicación', 
               B.IPDIRECCI 'Dirección', 
               B.IPTELEFON 'Tel 1', 
               B.IPTELMOVI 'Tel 2', 
               FORMAT(B.IPFECNACI, 'dd/MM/yyyy') 'Fecha Nacimiento', 
               B.IPNOMCOMP 'Nombre Paciente',
               CASE B.IPSEXOPAC
                   WHEN 1
                   THEN 'M'
                   WHEN 2
                   THEN 'F'
               END 'Sexo', 
               CAST(D.PESOPACIE AS INT) AS 'Peso (gr)', 
               D.TALLAPACI 'Talla (cm)',
               CASE B.IPTIPOPAC
                   WHEN 1
                   THEN 'Contributivo'
                   WHEN 2
                   THEN 'Subsidiado'
                   WHEN 3
                   THEN 'Vinculado'
                   WHEN 4
                   THEN 'Particular'
                   WHEN 5
                   THEN 'Otro'
                   WHEN 6
                   THEN 'Desplazado Reg. Contributivo'
                   WHEN 7
                   THEN 'Reg. Subsidiado'
                   WHEN 8
                   THEN 'Desplazado No Asegurado'
               END 'Regimen', 
               B.CODENTIDA, 
               F.Code 'Cod Entidad', 
               F.Name 'Entidad', 
               G.NOMBRE 'Modelo HC', 
               FORMAT(A.FECHISPAC, 'dd/MM/yyyy') AS 'Fecha de La historia'
        FROM.HCHISPACA A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            JOIN.INUBICACI C WITH(NOLOCK) ON C.AUUBICACI = B.AUUBICACI
            LEFT JOIN.HCEXFISIC D WITH(NOLOCK) ON D.NUMINGRES = A.NUMINGRES
                                                  AND D.NUMEFOLIO = A.NUMEFOLIO
            JOIN dbo.ADINGRESO E WITH(NOLOCK) ON E.NUMINGRES = A.NUMINGRES
            JOIN Contract.HealthAdministrator F WITH(NOLOCK) ON F.Id = E.GENCONENTITY
            JOIN.PRMODELOHC G WITH(NOLOCK) ON G.ID = A.IDMODELOHC
        WHERE A.IDMODELOHC IN(7, 8, 9)
            AND A.FECHISPAC BETWEEN @FechaIni AND @FechaFin;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado de historias clínicas correspondientes a los modelos de Primera Infancia, Infancia y Adolescencia (modelos HC 7, 8 y 9) para un rango de fechas indicado. Por cada historia clínica recuperada desde HCHISPACA, consolida la información demográfica del paciente (nombre completo, tipo y número de documento, fecha de nacimiento, sexo, dirección y teléfonos) desde INPACIENT, la ubicación geográfica desde INUBICACI, los datos de examen físico como peso y talla desde HCEXFISIC, el régimen y entidad pagadora (EPS/aseguradora) cruzando ADINGRESO con HealthAdministrator, y el nombre del modelo de historia clínica desde PRMODELOHC. Se utiliza para reportería asistencial y seguimiento poblacional de menores de edad atendidos en la institución, permitiendo identificar qué pacientes pediátricos tienen historia clínica registrada en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas de Primera Infancia, Infancia y Adolescencia atendidas en un rango de fechas, con datos demográficos del paciente, antropometría, régimen y entidad responsable de pago.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicio/fin) debe estar definido para filtrar FECHISPAC.; Deben existir modelos de historia clínica con ID 7, 8 y 9 correspondientes a Primera Infancia, Infancia y Adolescencia.; Cada historia debe tener ingreso asociado en ADINGRESO y entidad pagadora en Contract.HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan historias cuyo modelo pertenece al conjunto {7,8,9} (Primera Infancia, Infancia, Adolescencia).; El examen físico (peso/talla) es opcional: se conserva la fila aunque no exista registro en HCEXFISIC (LEFT JOIN).; El peso se reporta truncado a entero en gramos.; Las fechas se presentan en formato dd/MM/yyyy.; Todo paciente listado debe tener ubicación, ingreso y entidad pagadora vigentes (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Primera Infancia; Infancia; Adolescencia; Tipo de documento; Régimen de afiliación (Contributivo/Subsidiado/Vinculado/Particular/Desplazado); Entidad administradora de salud (EPS); Examen físico (peso y talla); Modelo de historia clínica; Ingreso asistencial; Ubicación del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.IDMODELOHC IN (7,8,9) y A.FECHISPAC BETWEEN @FechaIni AND @FechaFin, retorna fila con datos del paciente, ubicación, antropometría, régimen, entidad y modelo de HC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC entre ''1''..''11'' → Traduce el código a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC). else Cualquier otro valor se etiqueta como ''PE: Permiso especial de Permanencia''.; si IPSEXOPAC = 1 o 2 → Mapea a ''M'' o ''F'' respectivamente. else NULL (sin etiqueta de sexo).; si IPTIPOPAC entre 1..8 → Traduce a régimen: Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Reg. Subsidiado o Desplazado No Asegurado.; si A.IDMODELOHC IN (7,8,9) → Solo se incluyen historias clínicas de los modelos de Primera Infancia, Infancia y Adolescencia. else Se excluyen del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INUBICACI; dbo.HCEXFISIC; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia';
-- GO
