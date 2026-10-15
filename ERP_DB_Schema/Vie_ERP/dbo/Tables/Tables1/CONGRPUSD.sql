CREATE TABLE [dbo].[CONGRPUSD] (
    [CODUSUARI] CHAR (20) NOT NULL,
    [IDGRUPUSC] TINYINT   NOT NULL,
    CONSTRAINT [PK_CONGRPUSD] PRIMARY KEY CLUSTERED ([CODUSUARI] ASC),
    CONSTRAINT [FK_CONGRPUSD_CONGRPUSC] FOREIGN KEY ([IDGRUPUSC]) REFERENCES [dbo].[CONGRPUSC] ([IDGRUPUSC])
);


GO
ALTER TABLE [dbo].[CONGRPUSD] NOCHECK CONSTRAINT [FK_CONGRPUSD_CONGRPUSC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';

