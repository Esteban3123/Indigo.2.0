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
ALTER TABLE [EHR].[HCORDFRECUEMED] NOCHECK CONSTRAINT [FK_CODPRODUC];


GO
ALTER TABLE [EHR].[HCORDFRECUEMED] NOCHECK CONSTRAINT [FK_IDHCORDQUIMIO];


GO
ALTER TABLE [EHR].[HCORDFRECUEMED] NOCHECK CONSTRAINT [FK_SchemesId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la frecuencia (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificacion del registro (frecuencia)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que realiza la modificacion del registro (frecuencia) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora 1 que es la que estaba en la parametrización a la hora de hacer la Historia Clinica ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Real  5:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Real  4:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Real  3:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Real  2:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Real 1:  Aca llega la dosis que se calcula dependiendo del tipo de factor  +  Peso + IMC, en ocasiones esta dosis puede ser la misma con la dosis que esta paramterizada esto si el tipo de factor esta como No Aplica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis que estaba parametrizada a la hora de hacer la HC', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DOSISPAR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDFRECUEMED', @level2type = N'COLUMN', @level2name = N'ID';

