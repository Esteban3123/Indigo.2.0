CREATE TABLE [dbo].[HCAUDITORIA] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUCONS]    CHAR (15)     NULL,
    [CODPACQCON]    VARCHAR (25)  NULL,
    [FECHCONSU]     DATETIME      NULL,
    [FECHFINCONS]   DATETIME      NULL,
    [NOMMAQCONS]    VARCHAR (50)  NULL,
    [IPMAQCONS]     VARCHAR (50)  NULL,
    [MOVCONSULHCID] INT           NULL,
    [OBSERVACION]   VARCHAR (300) NULL,
    [INGRESO]       VARCHAR (15)  NULL,
    [FOLIOIN]       VARCHAR (10)  NULL,
    [ANONIMATO]     BIT           NULL,
    [FOLIOFINAL]    VARCHAR (10)  NULL,
    [UFUCODIGO]     CHAR (10)     NULL,
    CONSTRAINT [PK_HCAUDITORIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCAUDITORIA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCAUDITORIA_MOVCONSULHC] FOREIGN KEY ([MOVCONSULHCID]) REFERENCES [dbo].[MOVCONSULHC] ([Id])
);


GO
ALTER TABLE [dbo].[HCAUDITORIA] NOCHECK CONSTRAINT [FK_HCAUDITORIA_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCAUDITORIA] NOCHECK CONSTRAINT [FK_HCAUDITORIA_MOVCONSULHC];


GO
CREATE NONCLUSTERED INDEX [Consulta_Historia]
    ON [dbo].[HCAUDITORIA]([CODUSUCONS] ASC, [FECHCONSU] ASC)
    INCLUDE([CODPACQCON], [FECHFINCONS], [NOMMAQCONS], [IPMAQCONS], [MOVCONSULHCID], [OBSERVACION], [INGRESO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo del folio final del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica si el paciente esta bajo anonimato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ANONIMATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo del folio inicial del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FOLIOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de ingreso del paciente de la consulta actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'INGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la consulta ID de la tabla MOVCONSULHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'MOVCONSULHCID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion Ip  de la maquina de donde se consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'IPMAQCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la maquina de donde se consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'NOMMAQCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha finaliza la consulta de la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHFINCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'FECHCONSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del pacinte al que consultan ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODPACQCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario que consulta la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'CODUSUCONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAUDITORIA', @level2type = N'COLUMN', @level2name = N'ID';

