CREATE TABLE [Scheduling].[AppointmentHemocomponentsCUPS] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAppointmentHemocomponents] INT       NOT NULL,
    [CODSERIPS]                   CHAR (20) NOT NULL,
    [TypeLoad]                    TINYINT   NOT NULL,
    [IdRelatedDescription]        INT       NULL,
    CONSTRAINT [PK_AppointmentHemocomponentsCUPS] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_AppointmentHemocomponents_id] FOREIGN KEY ([IdAppointmentHemocomponents]) REFERENCES [Scheduling].[AppointmentHemocomponents] ([Id]),
    CONSTRAINT [Fk_INCUPSIPS_CODSERIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción adicional o detalle del CUP/hemoderivado (ej: compatibilidad, lote, vencimiento); referencia opcional para enriquecer información de la factura o RIPS', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción relacionada del CUP asociado al componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdRelatedDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cargue/registro del CUPS sanguíneo: 1=Automático (sistema carga), 2=Preguntar (solicita confirmación antes de registrar en factura/RIPS)', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cargue: 1:Automatico, 2:Preguntar antes de cargar', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'TypeLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS-SERIPS (CHAR 20) del hemoderivado/componente sanguíneo (ej: sangre total, plaquetas, plasma); referencia FK a INCUPSIPS. Usado en facturación y RIPS', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codserips del CUP asociado al componente sanguineo', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cita con componentes sanguíneos (hemoderivados/hemocomponentes); referencia FK a AppointmentHemocomponents. Vincula transfusión, donación o procedimiento hematológico', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de los componentes sanguineos ---> AppointmentHemocomponents', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'IdAppointmentHemocomponents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, identity) de la asociación entre cita/componente sanguíneo y código CUPS/SERIPS', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Códigos de servicios CUPS asociados a citas de hemocomponentes (transfusiones, hemoderivados). Relaciona cada cita de hemocomponentes con uno o más procedimientos facturables CUPS, indicando el tipo de carga y su descripción vinculada.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'TABLE', @level1name = N'AppointmentHemocomponentsCUPS';
