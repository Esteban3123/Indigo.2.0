CREATE TABLE [dbo].[HCCHEQUEOPAD] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)     NOT NULL,
    [IPCODPACI]       VARCHAR (25)  NOT NULL,
    [IDLISTACHEQUEO]  INT           NOT NULL,
    [IDLISTACHEQUEOD] INT           NOT NULL,
    [CHEQUEADO]       BIT           NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500) NULL,
    [CAMPOFECHA]      DATETIME      NULL,
    [URLADJUNTO]      VARCHAR (100) NULL,
    CONSTRAINT [PK_HCCHEQUEOPAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCHEQUEOPAD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC]),
    CONSTRAINT [FK_HCCHEQUEOPAD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCCHEQUEOPAD] NOCHECK CONSTRAINT [FK_HCCHEQUEOPAD_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCHEQUEOPAD] NOCHECK CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACC];


GO
ALTER TABLE [dbo].[HCCHEQUEOPAD] NOCHECK CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACD];


GO
ALTER TABLE [dbo].[HCCHEQUEOPAD] NOCHECK CONSTRAINT [FK_HCCHEQUEOPAD_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define si fue seleccionada la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Item deatlle de la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificaión del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'ID';

