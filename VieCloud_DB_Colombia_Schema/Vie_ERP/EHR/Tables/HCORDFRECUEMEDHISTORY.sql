CREATE TABLE [EHR].[HCORDFRECUEMEDHISTORY] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [IDHCORDFRECUEMED]         INT             NOT NULL,
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
    [State]                    INT             NOT NULL,
    CONSTRAINT [PK_HCORDFRECUEMEDHISTORY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CODPRODUCH] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_SchemesIdH] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de frecuencia medicamentosa en ciclo de quimioterapia: 1=Original de ordenamiento, 2=Adicionado, 3=Modificado, 4=Eliminado. Auditoría de cambios en prescripción oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la frecuencia de los medicamentos del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del registro de frecuencia medicamentosa del ciclo de quimioterapia. Timestamp de auditoría.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se modifica la frecuencia de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional de la salud (médico oncólogo, enfermero) que modificó la frecuencia medicamentosa del ciclo. Tipo CHAR(20), código profesional.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que modifica la frecuencia de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta hora de administración parametrizada en el momento de la historia clínica. Hora de aplicación del medicamento según protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta hora de administración parametrizada en el momento de la historia clínica. Hora de aplicación del medicamento según protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera hora de administración parametrizada en el momento de la historia clínica. Hora de aplicación del medicamento según protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda hora de administración parametrizada en el momento de la historia clínica. Hora de aplicación del medicamento según protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera hora de administración parametrizada en el momento de la historia clínica. Hora de aplicación del medicamento según protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis real calculada 5: valor en mg/kg ajustado por peso, IMC, factor de conversión. Puede coincidir con DOSISPAR5 si factor=No Aplica. Quimioterapia personalizada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  5:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis real calculada 4: valor en mg/kg ajustado por peso, IMC, factor de conversión. Puede coincidir con DOSISPAR4 si factor=No Aplica. Quimioterapia personalizada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  4:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis real calculada 3: valor en mg/kg ajustado por peso, IMC, factor de conversión. Puede coincidir con DOSISPAR3 si factor=No Aplica. Quimioterapia personalizada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  3:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis real calculada 2: valor en mg/kg ajustado por peso, IMC, factor de conversión. Puede coincidir con DOSISPAR2 si factor=No Aplica. Quimioterapia personalizada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real  2:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis real calculada 1: valor en mg/kg ajustado por peso, IMC, factor de conversión. Puede coincidir con DOSISPAR1 si factor=No Aplica. Quimioterapia personalizada.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Real 1:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis parametrizada 5 en el protocolo de quimioterapia al momento de la historia clínica. Valor mg/kg estándar del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis parametrizada 4 en el protocolo de quimioterapia al momento de la historia clínica. Valor mg/kg estándar del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis parametrizada 3 en el protocolo de quimioterapia al momento de la historia clínica. Valor mg/kg estándar del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis parametrizada 2 en el protocolo de quimioterapia al momento de la historia clínica. Valor mg/kg estándar del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis parametrizada 1 en el protocolo de quimioterapia al momento de la historia clínica. Valor mg/kg estándar del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico (medicamento quimioterapéutico). CHAR(20), referencia a catálogo de fármacos oncológicos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del día dentro del ciclo de quimioterapia (1-N). Día de administración en la frecuencia medicamentosa.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del ciclo de quimioterapia (ciclo 1, 2, 3...). Ordinal de repetición del esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/orden maestra de quimioterapia. Llave foránea a registro principal de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema quimioterapéutico (protocolo). Llave foránea a tabla de esquemas de tratamiento estandarizados.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de frecuencia medicamentosa. Llave foránea a tabla HCORDFRECUEMED, referencia a la prescripción principal.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDFRECUEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla HCORDFRECUEMED', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDFRECUEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDFRECUEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro histórico de frecuencia medicamentosa. INT IDENTITY, contador secuencial de auditoría.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de cambios en la frecuencia de medicamentos de órdenes de quimioterapia. Registra las modificaciones realizadas a las dosis, horarios y parámetros de cada medicamento por ciclo y día dentro de un esquema de tratamiento oncológico, permitiendo trazabilidad de los ajustes efectuados.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMEDHISTORY';
