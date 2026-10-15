CREATE TABLE [dbo].[INPLANTID] (
    [CODPLANTI] CHAR (5)   NOT NULL,
    [PKCONTROL] CHAR (30)  NOT NULL,
    [PKTIPCONT] INT        NOT NULL,
    [PKVALORCO] CHAR (300) NOT NULL,
    CONSTRAINT [PK_INPLANTID] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC, [PKCONTROL] ASC),
    CONSTRAINT [FK_INPLANTID_INPLANTIC] FOREIGN KEY ([CODPLANTI]) REFERENCES [dbo].[INPLANTIC] ([CODPLANTI])
);


GO
ALTER TABLE [dbo].[INPLANTID] NOCHECK CONSTRAINT [FK_INPLANTID_INPLANTIC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Reservado Arquitecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKVALORCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Reservado Arquitecto:  1: TE  2: BE  3: DE  4: PC  5: RG  6: LE  7: ME  8: CE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKTIPCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Reservado Arquitecto  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'PKCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Plantilla para Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTID', @level2type = N'COLUMN', @level2name = N'CODPLANTI';

