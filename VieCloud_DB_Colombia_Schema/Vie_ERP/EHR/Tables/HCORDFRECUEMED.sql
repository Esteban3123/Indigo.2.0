CREATE TABLE [EHR].[HCORDFRECUEMED] (
    [ID]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]                INT             NOT NULL,
    [IDHCORDQUIMIO]            INT             NOT NULL,
    [CICLO]                    INT             NOT NULL,
    [DIA]                      INT             NOT NULL,
    [CODPRODUC]                CHAR (20)       NOT NULL,
    [DOSISPAR1]                NUMERIC (18, 2) NULL,
    [DOSISPAR2]                NUMERIC (18, 2) NULL,
    [DOSISPAR3]                NUMERIC (18, 2) NULL,
    [DOSISPAR4]                NUMERIC (18, 2) NULL,
    [DOSISPAR5]                NUMERIC (18, 2) NULL,
    [DOSIS1]                   NUMERIC (18, 2) NULL,
    [DOSIS2]                   NUMERIC (18, 2) NULL,
    [DOSIS3]                   NUMERIC (18, 2) NULL,
    [DOSIS4]                   NUMERIC (18, 2) NULL,
    [DOSIS5]                   NUMERIC (18, 2) NULL,
    [HORA1]                    DATETIME        NULL,
    [HORA2]                    DATETIME        NULL,
    [HORA3]                    DATETIME        NULL,
    [HORA4]                    DATETIME        NULL,
    [HORA5]                    DATETIME        NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             CONSTRAINT [DF_HCORDFRECUEMED_State] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCORDFRECUEMED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CODPRODUC] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_IDHCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_SchemesId] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);




GO





GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la frecuencia medicamentosa (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado). INT, auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la frecuencia (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del registro de frecuencia de medicamento. DATETIME, rastro de auditoría, trazabilidad de cambios en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro (frecuencia)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del profesional de salud que realizó la modificación de la frecuencia. CHAR(20), responsable del cambio, médico oncólogo o farmacéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza la modificacion del registro (frecuencia) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta hora programada para la administración del medicamento según parametrización en historia clínica. DATETIME, horario de dosis quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta hora programada para la administración del medicamento según parametrización en historia clínica. DATETIME, horario de dosis quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera hora programada para la administración del medicamento según parametrización en historia clínica. DATETIME, horario de dosis quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda hora programada para la administración del medicamento según parametrización en historia clínica. DATETIME, horario de dosis quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera hora programada para la administración del medicamento según parametrización en historia clínica. DATETIME, horario de dosis quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis real calculada (factor + peso + IMC), puede coincidir con dosis parametrizada si factor es No Aplica. NUMERIC(18,2), dosis efectiva quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  5:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis real calculada (factor + peso + IMC), puede coincidir con dosis parametrizada si factor es No Aplica. NUMERIC(18,2), dosis efectiva quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  4:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis real calculada (factor + peso + IMC), puede coincidir con dosis parametrizada si factor es No Aplica. NUMERIC(18,2), dosis efectiva quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  3:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis real calculada (factor + peso + IMC), puede coincidir con dosis parametrizada si factor es No Aplica. NUMERIC(18,2), dosis efectiva quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  2:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis real calculada (factor + peso + IMC), puede coincidir con dosis parametrizada si factor es No Aplica. NUMERIC(18,2), dosis efectiva quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real 1:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis parametrizada en el esquema de quimioterapia al momento de crear la historia clínica. NUMERIC(18,2), dosis base protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis parametrizada en el esquema de quimioterapia al momento de crear la historia clínica. NUMERIC(18,2), dosis base protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis parametrizada en el esquema de quimioterapia al momento de crear la historia clínica. NUMERIC(18,2), dosis base protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis parametrizada en el esquema de quimioterapia al momento de crear la historia clínica. NUMERIC(18,2), dosis base protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis parametrizada en el esquema de quimioterapia al momento de crear la historia clínica. NUMERIC(18,2), dosis base protocolo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del medicamento, producto farmacéutico o quimioterápico. CHAR(20), FK a IHLISTPRO, medicamento, droga, fármaco.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de día dentro del ciclo de quimioterapia en que se administra la frecuencia. INT, día de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del ciclo de quimioterapia. INT, ciclo de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de cabecera de quimioterapia asociada. INT, FK a HCORDQUIMIO, orden principal, prescripción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema terapéutico de quimioterapia. INT, FK a Schemes, protocolo oncológico, plan de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la frecuencia medicamentosa. INT IDENTITY, clave primaria, consecutivo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia y dosificación de medicamentos dentro de un esquema de quimioterapia. Registra por ciclo y día los productos oncológicos a administrar, con sus dosis parciales, dosis totales y horarios de aplicación (hasta 5 tomas por día).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED';
