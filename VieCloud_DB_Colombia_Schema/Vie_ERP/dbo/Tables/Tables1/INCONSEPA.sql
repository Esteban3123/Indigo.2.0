CREATE TABLE [dbo].[INCONSEPA] (
    [IDPOBLACI] CHAR (1)     NOT NULL,
    [IDDOCUMEN] VARCHAR (25) NOT NULL,
    [CONSECUTI] INT          NOT NULL,
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_INCONSEPA] PRIMARY KEY CLUSTERED ([IDPOBLACI] ASC, [IDDOCUMEN] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de auditoría (INT IDENTITY). Registro de trazabilidad y control de cambios en la tabla de poblaciones especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la trazibilidad de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo (INT). Número secuencial correlativo para ordenamiento de registros dentro de cada población especial y documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Documento (VARCHAR 25). Identificador variable según población: para grupos 0-4 es código Departamento+Municipio del centro de atención; para grupo 5 (menores) es cédula/identificación de madre o cabeza de hogar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDDOCUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Documento:    -> Para las poblaciones del 0 al 4, es el codigo del Departamento + Codigo del Municipio (tomados del centro de atencion seleccionado)    -> Para la poblacion 5, es la identificacion de la madre o la identificacion de la cabeza de hogar  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDDOCUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDDOCUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Población Especial (CHAR 1). Clasificación de vulnerabilidad: 0=Anciano en protección, 1=Indígena sin RNEC, 2=Indigente, 3=Menor ICBF, 4=Menor indígena sin RNEC, 5=Recién nacido ≤30 días sin identificar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDPOBLACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de Poblacion Especial:  
0. Personas de la tercera edad en protección de ancianatos.  
1. Comunidad Indígena que no este identificada por la Registraduría Nacional del Estado Civil  
2. Población indigente  
3. Población infantil a cargo del ICBF  
4. Comunidad indígena menor de edad no identificada por la RNEC  
5. Menor de edad recién nacido vivo sin identificar al infante con edad menor o igual a 30 días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDPOBLACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA', @level2type = N'COLUMN', @level2name = N'IDPOBLACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de consecutivos por tipo de población y documento. Permite controlar la numeración secuencial asignada a cada paciente o persona según su categoría poblacional e identificación, utilizado en procesos de admisión, historia clínica o generación de registros únicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSEPA';
