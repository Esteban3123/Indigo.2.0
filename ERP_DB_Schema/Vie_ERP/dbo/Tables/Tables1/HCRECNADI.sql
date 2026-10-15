CREATE TABLE [dbo].[HCRECNADI] (
    [NUMCONSEC] INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECREC] INT      NOT NULL,
    [CODDIAGNO] CHAR (4) NOT NULL,
    CONSTRAINT [PK_HCRECNADI] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_HCRECNADI_HCRECINAC] FOREIGN KEY ([CONSECREC]) REFERENCES [dbo].[HCRECINAC] ([NUMCONSEC]),
    CONSTRAINT [FK_HCRECNADI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCRECNADI] NOCHECK CONSTRAINT [FK_HCRECNADI_HCRECINAC];


GO
ALTER TABLE [dbo].[HCRECNADI] NOCHECK CONSTRAINT [FK_HCRECNADI_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'CONSECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECNADI', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';

