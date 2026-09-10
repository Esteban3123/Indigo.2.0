CREATE TABLE [dbo].[PYPPARAHISTPERMI] (
    [ID]             INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO] INT                                                                           NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[PYPPARAHISTPERMI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud (médico, enfermero, especialista) autorizado para ingresar datos en la historia clínica parametrizada. Campo enmascarado (PII). Identificación única del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Profesional que puede ingresara la HC parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la tabla PYPPARAHISTO. Referencia a la historia clínica parametrizada padre. Clave foránea que vincula permisos a configuración de historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion de la tabla de Historia parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (IDENTITY) de la tabla. Clave primaria única que identifica cada registro de permiso de profesional en historia parametrizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de permisos por profesional de la salud. Guarda el detalle de qué profesionales (médicos, enfermeros u otros) estuvieron asociados a un historial de parámetros o permisos en el sistema de nómina/parametrización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTPERMI';
