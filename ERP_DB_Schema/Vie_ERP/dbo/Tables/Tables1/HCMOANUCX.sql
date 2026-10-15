CREATE TABLE [dbo].[HCMOANUCX] (
    [CODMOTANU] CHAR (3)      NOT NULL,
    [DESMOTANU] VARCHAR (200) NOT NULL,
    [ESTMOTANU] BIT           NOT NULL,
    CONSTRAINT [PK_HCMOANUCX] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'ESTMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Motivo Cancelacion de Cirugias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANUCX', @level2type = N'COLUMN', @level2name = N'CODMOTANU';

