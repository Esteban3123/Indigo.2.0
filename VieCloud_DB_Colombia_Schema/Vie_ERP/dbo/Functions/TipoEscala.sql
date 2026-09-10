

CREATE FUNCTION [dbo].[TipoEscala] 
(
@CodigoEscala AS integer
)

RETURNS varchar(500)
AS
BEGIN

declare @NombreEscala varchar(500)

SELECT @NombreEscala =  CASE @CodigoEscala
			WHEN 1 THEN 'Escala Cage'
			WHEN 2 THEN 'Escala Apgar Familiar'
			WHEN 3 THEN 'Escala EDPS'
			WHEN 4 THEN 'Escala Biopsicosocial'
			WHEN 5 THEN 'Escala tamizaje violencia doméstica'
			WHEN 6 THEN 'Escala riesgo Framingham	   '
			WHEN 7 THEN 'Escala Morisky					   '
			WHEN 8 THEN 'Test Findrisc						   '
			WHEN 9 THEN 'Test Minimental					   '
			WHEN 10 THEN 'Test dependencia nicotina   '
			WHEN 11 THEN 'Escala Tanner desarrollo mamario'
			WHEN 12 THEN 'Escala Tanner desarrollo vello pubiano (Mujer)'
			WHEN 13 THEN 'Escala Tanner desarrollo genital'
			WHEN 14 THEN 'Escala Tanner desarrollo vello pubiano (Hombre)'
			WHEN 15 THEN 'Escala Wagner					   '
			WHEN 16 THEN 'Escala modificada Disenea	   '
			WHEN 17 THEN 'Escala CAT			   '
			WHEN 18 THEN 'Exacerbaciones					   '
			WHEN 19 THEN 'Clasificacion EPOC				   '
			WHEN 20 THEN 'Test Goodenough			   '
			WHEN 21 THEN 'Clasificación GOLD						   '
			WHEN 22 THEN 'Escala abreviada desarrollo'
			WHEN 23 THEN 'Escala TISS 28						   '
			WHEN 24 THEN 'Escala Braden						   '
			WHEN 25 THEN 'Escala ApacheII					   '
			WHEN 26 THEN 'Escala karnosfky				   '
			WHEN 27 THEN 'ECOG									   '
			WHEN 28 THEN 'Escala Nems						   '
			WHEN 29 THEN 'Escala Glasgow Mayor 5 Años'
			WHEN 30 THEN 'Escala Glasgow de 1 a 5 Años'
			WHEN 31 THEN 'Escala Glasgow Menor 1 Año '
			WHEN 32 THEN 'Escala SOFA							   '
			WHEN 33 THEN 'Escala Charlson					   '
			WHEN 34 THEN 'Escala SAPS3						   '
			WHEN 35 THEN 'Barthel							   '
			WHEN 36 THEN 'Escala Morse						   '
			WHEN 37 THEN 'Escala Macdems					   '
			WHEN 38 THEN 'Escala NSRAS						   '
			WHEN 39 THEN 'MSTS									   '
			WHEN 40 THEN 'Sad Persons						   '
			WHEN 41 THEN 'Escala de Beck						   '
			WHEN 42 THEN 'Escala de Zarit						   '
			WHEN 43 THEN 'Cuestionario RQC						   '
			WHEN 44 THEN 'Indice de placa bacteriana'
			WHEN 45 THEN 'Escala VALE							   '
			WHEN 46 THEN 'Escala RASS							   '
			WHEN 47 THEN 'Escala Downton						   '
			WHEN 48 THEN 'Escala Norton						   '
			WHEN 49 THEN 'Escala VAS (Visual Analogue Scale)	   '
			WHEN 50 THEN 'Escala Nutrición					   '
			WHEN 51 THEN 'Escala SQR								   '
			WHEN 52 THEN 'Escala M-CHAT						   '
			WHEN 53 THEN 'Escala WHOOLEY					   '
			WHEN 54 THEN 'Escala Audit-Alcholismo				   '
			WHEN 55 THEN 'Escala de Fragilidad de Linda Fried'
			WHEN 56 THEN 'Escala de Autonomía Lawton - Brody'
			WHEN 57 THEN 'Escala GAD								   '
			WHEN 58 THEN 'Escala MNA								   '
			WHEN 59 THEN 'Escala MNA Simplificada	   '
			WHEN 60 THEN 'Escala Assist						   '
			WHEN 61 THEN 'Escala News							   '
			WHEN 62 THEN 'Escala CHA2DS2_VASc			   '
			WHEN 63 THEN 'Escala CRUSADE						   '
			WHEN 64 THEN 'Escala HAS_BLED					   '
			WHEN 65 THEN 'Escala HEMORR2HAGES			   '
			WHEN 66 THEN 'Escala EUROSCOREII				   '
			WHEN 67 THEN 'Escala NYHA							   '
			WHEN 68 THEN 'Escala KILLIP						   '
			WHEN 69 THEN 'Escala PADUA							   '
			WHEN 70 THEN 'Escala CAPRINI						   '
			WHEN 71 THEN 'Escala MUST							   '
			WHEN 72 THEN 'Escala STRONG KIDS			   '
			WHEN 73 THEN 'Escala VALORACIÓN GLOBAL SUBJETIVA DEL ESTADO NUTRICIONAL'
			WHEN 74 THEN 'eEscala TIMI CEST				   '
			WHEN 75 THEN 'Escala WELLS TVP				   '
			WHEN 76 THEN 'Escala WELLS TEP				   '
			WHEN 77 THEN 'Escala NPC							   '
			WHEN 78 THEN 'Escala GRACE						   '
			WHEN 79 THEN 'Escala TIMI SEST				   '
			WHEN 80 THEN 'Escala ANTHONISEN				   '
			WHEN 81 THEN 'Escala DAS 28						   '
			WHEN 82 THEN 'Escala mRS								   '
			WHEN 83 THEN 'Escala HAQ								   '
			WHEN 84 THEN 'Escala ASPECT						   '
			WHEN 85 THEN 'Escala Abreviada Desarrollo V3'
			WHEN 86 THEN 'Escala Indice O Leary			   '
			WHEN 87 THEN 'Escala NIHSS"							   '
			WHEN 88 THEN 'Escala Humpty Dumpty			   '
			WHEN 89 THEN 'Escala Riesgo Enfermedades Potencialmente Transmisibles'
			WHEN 90 THEN 'Escala PIPP-R						   '
			WHEN 91 THEN 'Escala FLACC						   '
			WHEN 92 THEN 'Escala OFRAS'
			WHEN 93 THEN 'Escala FPS-R (Faces Pain Scale-Revised)'
			WHEN 94 THEN 'Escala Fugulin'
			WHEN 95 THEN 'Escala CPOT'

		END

RETURN @NombreEscala

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe un código numérico de escala clínica y devuelve su nombre completo en texto. Traduce identificadores numéricos a nombres de instrumentos de valoración médica y de enfermería utilizados en la historia clínica del paciente, tales como escalas de riesgo cardiovascular, nutricional, de caídas, de dolor, de desarrollo, de salud mental y de cuidado crítico (Apache II, SOFA, Glasgow, Braden, Barthel, entre otras). Se usa para mostrar el nombre legible de la escala aplicada al paciente en reportes, formularios clínicos y resultados de evaluaciones asistenciales, evitando almacenar el nombre completo en cada registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoEscala';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoEscala';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de escala clínica a su nombre legible, centralizando el catálogo de escalas y tests de valoración usados en la historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe estar en el rango catalogado (1 a 95); de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La función es determinista: a un mismo código siempre devuelve el mismo nombre.; No accede a tablas: el catálogo de escalas está hardcodeado en el CASE.; Cubre escalas de múltiples dominios clínicos (riesgo cardiovascular, dolor, nutrición, neurológicas, salud mental, desarrollo infantil, geriatría, UCI, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escalas de valoración clínica; Tamizaje de violencia doméstica; Riesgo cardiovascular (Framingham, CHA2DS2-VASc, HAS-BLED, GRACE, TIMI, KILLIP, NYHA); Adherencia (Morisky); Dependencia a nicotina y alcohol (Findrisc, AUDIT, ASSIST); Desarrollo puberal (Tanner); EPOC (CAT, GOLD, mMRC, Anthonisen); Valoración neurológica (Glasgow, NIHSS, Mini-mental, ASPECT, mRS); Cuidado crítico/UCI (APACHE II, SOFA, SAPS3, NEMS, TISS-28, RASS, CPOT); Riesgo de caídas (Downton, Morse, Humpty Dumpty); Úlceras por presión (Braden, Norton, NSRAS); Valoración funcional/geriátrica (Barthel, Lawton-Brody, Fried, Charlson, Karnofsky, ECOG); Nutrición (MNA, MUST, STRONG KIDS, VGS); Salud mental (Beck, Zarit, GAD, Whooley, SAD PERSONS, SRQ); Pediatría/desarrollo (Apgar, Goodenough, Escala Abreviada Desarrollo, M-CHAT, RQC, PIPP-R, FLACC, FPS-R); Dolor (VAS, FLACC, FPS-R, CPOT, PIPP-R); Tromboembolismo (Wells TVP/TEP, Padua, Caprini); Riesgo quirúrgico (EuroSCORE II); Pie diabético (Wagner); Odontología (Índice de placa bacteriana); Reumatología (DAS28, HAQ)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve el nombre textual de la escala según el código mediante un CASE; si el código no coincide con ningún WHEN, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de escala entre 1 y 95 → Retorna el nombre específico de la escala/test correspondiente (Cage, Apgar Familiar, EDPS, Glasgow por edad, Braden, Norton, Apache II, etc.) else Retorna NULL al no coincidir con ningún caso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoEscala';
GO
