CREATE TABLE [dbo].[ADEML3047] (
    [AUTO]      INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT        NOT NULL,
    [ENTIEMAIL] CHAR (100) NOT NULL,
    CONSTRAINT [PK_ADEML3047] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_ADEML3047_ADPA3047E] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADPA3047E] ([AUTO])
);


GO
ALTER TABLE [dbo].[ADEML3047] NOCHECK CONSTRAINT [FK_ADEML3047_ADPA3047E];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'E-Mail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEML3047', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla ADPA3047E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEML3047', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEML3047', @level2type = N'COLUMN', @level2name = N'AUTO';

