CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricoPerfilFarmacoteerapeutico]
(
  @Identificacion as varchar(25)
)

AS
BEGIN
  SET NOCOUNT ON;
 
		select c.ID as IDHCNUTPAREC, FECHAVERIFICA AS Fechaverifica, Rtrim(B.NOMMEDICO) AS Medico,Rtrim(OBSERVERIFICA) as Observacion,c.NUMINGRES  as Ingreso,
		RTRIM(d.DESESPECI) as EspecialidadTratante, RTRIM(v.NOMDIAGNO) as Diagnostico,t.Description as DosisUnitaria, Rtrim(n.UFUDESCRI) AS UFUDESCRI, Rtrim(m.NOMCENATE) AS NOMCENATE
		from dbo.HCNUTPAREC C
			INNER JOIN HCHISPACA x  with(noLock)  ON x.ID = c.IDHCHISPACA 
			INNER JOIN INPROFSAL B with(noLock)  ON C.CODPROSAL = B.CODPROSAL
			INNER JOIN INESPECIA D with(noLock)  ON D.CODESPECI = x.CODESPTRA
			INNER JOIN INDIAGNOS v with(noLock)  ON v.CODDIAGNO = x.CODDIAGNO
			INNER JOIN MixingStation.UnitDoseType t with(noLock)  on t.Id = c.IDUNITDOSETYPE 
			INNER JOIN INUNIFUNC n with(noLock)  on n.UFUCODIGO = x.UFUCODIGO 
			INNER JOIN ADCENATEN m with(noLock)  on m.CODCENATE = x.CODCENATE  
		where AGRUPAQUETE is not null  and c.IPCODPACI = @Identificacion

		UNION ALL 
                SELECT '' as IDHCNUTPAREC, DocumentDate as Fechaverifica,RTRIM(C.NOMMEDICO) AS Medico, '' as Observacion,(A.NUMINGRES) as Ingreso,
				RTRIM(F.DESESPECI) AS EspecialidadTratante, RTRIM(H.NOMDIAGNO) as Diagnostico, '' AS DosisUnitaria, RTRIM(E.UFUDESCRI) AS UFUDESCRI, Rtrim(D.NOMCENATE) AS NOMCENATE               
                FROM MedicalHistory.ChemicalPharmaceuticalNotes AS A
				INNER JOIN INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
				INNER JOIN INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
				INNER JOIN ADCENATEN D ON A.CODCENATE=D.CODCENATE
				INNER JOIN INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO
				INNER JOIN INDIAGNOP P on A.NUMINGRES=P.NUMINGRES and A.IPCODPACI=P.IPCODPACI and P.CODDIAPRI = 1 
				INNER JOIN INDIAGNOS H on P.CODDIAGNO= H.CODDIAGNO
				LEFT OUTER JOIN INESPECIA F ON C.CODESPEC1=F.CODESPECI
			    WHERE A.IPCODPACI= @Identificacion
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial completo del perfil farmacoterapéutico de un paciente, identificado por su cédula o documento. Combina dos fuentes: los registros de nutrición parenteral y recetas con dosis unitaria (HCNUTPAREC / HCHISPACA) y las notas químico-farmacéuticas (ChemicalPharmaceuticalNotes), unificando para cada evento el número de ingreso, la fecha de verificación, el médico tratante, la especialidad tratante, el diagnóstico (CIE-10), el tipo de dosis unitaria, la unidad funcional (servicio o sala) y el centro de atención. Se utiliza para visualizar el seguimiento farmacológico completo del paciente a lo largo de sus distintos ingresos y atenciones, apoyando la revisión clínica y farmacéutica del tratamiento histórico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico del perfil farmacoterapéutico de un paciente, unificando registros de nutrición parenteral verificada y notas químico-farmacéuticas, con datos clínicos asociados (médico, especialidad, diagnóstico, unidad funcional y centro de atención).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere identificación del paciente (IPCODPACI) para filtrar los registros.; Las tablas maestras (profesional, especialidad, diagnóstico, unidad funcional, centro de atención, tipo de dosis unitaria) deben tener los códigos referenciados, ya que los JOIN son INNER en ambas consultas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes de nutrición que han sido agrupadas en paquete (AGRUPAQUETE IS NOT NULL).; Para las notas químico-farmacéuticas, solo se toma el diagnóstico principal del ingreso (CODDIAPRI=1).; Los registros de notas químico-farmacéuticas no exponen ID ni observación (devueltos como cadena vacía) ni dosis unitaria.; La especialidad en las notas químico-farmacéuticas se obtiene de la especialidad principal del profesional (CODESPEC1), pudiendo ser NULL por LEFT JOIN.; La especialidad en la nutrición parenteral se obtiene de la especialidad tratante del episodio (HCHISPACA.CODESPTRA).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Perfil farmacoterapéutico; Nutrición parenteral/enteral; Notas químico-farmacéuticas; Dosis unitaria; Diagnóstico principal; Especialidad tratante; Unidad funcional; Centro de atención; Ingreso/episodio del paciente; Verificación farmacéutica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve la unión (UNION ALL) de: (1) registros de HCNUTPAREC del paciente cuyo AGRUPAQUETE no es NULL, y (2) notas de MedicalHistory.ChemicalPharmaceuticalNotes del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCNUTPAREC.AGRUPAQUETE IS NOT NULL y IPCODPACI coincide con el parámetro → Incluye el registro de nutrición parenteral con su observación, fecha de verificación y tipo de dosis unitaria.; si INDIAGNOP.CODDIAPRI = 1 (diagnóstico principal del ingreso) → Asocia la nota químico-farmacéutica al diagnóstico principal del ingreso del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNUTPAREC; dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INDIAGNOS; MixingStation.UnitDoseType; dbo.INUNIFUNC; dbo.ADCENATEN; MedicalHistory.ChemicalPharmaceuticalNotes; dbo.INPACIENT; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoPerfilFarmacoteerapeutico';
-- GO
