CREATE TABLE [EHR].[HCORDMEDICAMHISTORY] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [IDHCORDMEDICAM]           INT             NOT NULL,
    [SchemesId]                INT             NOT NULL,
    [IDHCORDQUIMIO]            INT             NOT NULL,
    [ATCEntityId]              INT             NULL,
    [CICLO]                    INT             NOT NULL,
    [DIA]                      INT             NOT NULL,
    [CODPRODUC]                CHAR (20)       NOT NULL,
    [CODVIAADM]                VARCHAR (20)    NOT NULL,
    [DOSISPROD]                NUMERIC (18, 2) NOT NULL,
    [CODUNIMED]                VARCHAR (20)    NOT NULL,
    [TIPOFACTOR]               INT             NOT NULL,
    [CANTIDAD]                 INT             NOT NULL,
    [INDICE]                   INT             NOT NULL,
    [DOSISTEORICA]             NUMERIC (18, 2) NOT NULL,
    [INSTRUADMINIS]            VARCHAR (MAX)   NULL,
    [CODDILUYENTE]             CHAR (20)       NULL,
    [VOLUMENFINAL]             NUMERIC (18, 2) NULL,
    [CANTIDADDILU]             INT             NULL,
    [MEDICAMENTOENCASA]        INT             NULL,
    [HORAFRECUDIA]             DATETIME        NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             NOT NULL,
    CONSTRAINT [PK_HCORDMEDICAMHISTORY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM1] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM2] FOREIGN KEY ([CODDILUYENTE]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM1];


GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM2];


GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_IHLISTPRO];


GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_Schemes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle de los medicamentos del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que que se modifica el detalle de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que modifica el detalle de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que me va contener la Fecha de la frecuencia:    Ejemplo: (10:00 - 13:00 - 18:00)    Dia 1 hora 10:00  Dia 1 hora 13:00  Dia 1 hora 18:00      Este campo viene diligenciado solo para los medicamentos que son marcados con frecuencia es decir que en el mismo dia se aplica varias veces el medicamento.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se administra en casa?    1 - Si   2 - No ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Solicitado  2 - Administrado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Volumen final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la instrucion adminitrar', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Dosis teorica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Indice medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del medicamento por dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1-Superficie corporal total  2-Indice de masa corporal  3-Peso  4-Sin factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concetración ó Dosis del producto ordenada', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Ciclo  medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla de VIE ATCEntity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del orden esquema de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla HCORDMEDICAM', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDMEDICAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ID';

