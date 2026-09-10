
CREATE VIEW [dbo].[ViewSurgicalEquipment]
AS
select CONSEEQQX,NUMEFOLIO,IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,a.CODPROSAL,QXPRINCIP,TIPOEQUIP,PERFILPRO, PS.NOMMEDICO from 
dbo.HCQXEQUIP a 
inner join dbo.INPROFSAL AS PS with(nolock) on PS.CODPROSAL = a.CODPROSAL

union all
select CONSEEQQX,a.NUMEFOLIO,a.IPCODPACI,INGMH.NUMINGRES,a.CODCENATE,a.UFUCODIGO,a.CODPROSAL,QXPRINCIP,TIPOEQUIP,PERFILPRO , PS.NOMMEDICO
from dbo.HCQXEQUIP a
inner join dbo.HCINGRESORECNAC INGMH with(nolock) on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN with(nolock) on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING with(nolock) on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
inner join dbo.INPROFSAL AS PS with(nolock) on PS.CODPROSAL = a.CODPROSAL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el equipo quirúrgico (cirujanos, anestesiólogos, instrumentadores y demás roles) asignado a cada procedimiento quirúrgico, incluyendo el nombre completo del profesional de salud, su rol en la cirugía (si es cirujano principal o no), el tipo de equipo y el perfil profesional. Integra los registros de equipos quirúrgicos (HCQXEQUIP) con el maestro de profesionales (INPROFSAL) para obtener el nombre del médico. Adicionalmente, extiende la consulta para cubrir los casos de recién nacidos, vinculando el ingreso del hijo con el ingreso de la madre mediante las tablas HCINGRESORECNAC, HCRECINAC y ADINGRESO, de modo que el equipo quirúrgico quede correctamente asociado tanto al ingreso de la madre como al del recién nacido. Se usa en reportería clínica quirúrgica, auditoría de salas de cirugía y verificación del equipo médico participante en cada intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgicalEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgicalEquipment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los equipos quirúrgicos asignados a procedimientos, unificando los casos de pacientes con ingreso propio y los de recién nacidos vinculados al ingreso de la madre, incluyendo el nombre del profesional responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada equipo quirúrgico debe tener un profesional de la salud existente en el maestro de profesionales; Para el caso de recién nacidos debe existir el vínculo entre ingreso de la madre y el ingreso del hijo, así como el registro clínico del recién nacido y su admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todo equipo quirúrgico retornado tiene asociado un profesional de la salud con nombre; La vista nunca muestra equipos quirúrgicos sin profesional vinculado (INNER JOIN con INPROFSAL); Los equipos quirúrgicos de recién nacidos se exponen junto con los de pacientes regulares en un único conjunto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'equipo quirúrgico; cirugía/procedimiento quirúrgico; profesional de la salud / médico; ingreso/admisión del paciente; recién nacido; vínculo madre-hijo en hospitalización; unidad funcional; centro de atención; perfil profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión (UNION ALL) de equipos quirúrgicos: una rama con el ingreso directo del paciente y otra rama donde el ingreso se obtiene a través del vínculo madre-recién nacido (NUMINGRESHIJO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El registro de HCQXEQUIP corresponde a un ingreso normal del paciente → Se reporta el equipo quirúrgico tomando NUMINGRES directamente del registro y enlazando al profesional; si El registro de HCQXEQUIP referencia a un ingreso de recién nacido (NUMINGRES coincide con NUMINGRESHIJO en HCINGRESORECNAC) → Se reporta el equipo quirúrgico cruzando con el registro de recién nacido y la admisión asociada para obtener el NUMINGRES del hijo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXEQUIP; dbo.INPROFSAL; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalEquipment';
GO
