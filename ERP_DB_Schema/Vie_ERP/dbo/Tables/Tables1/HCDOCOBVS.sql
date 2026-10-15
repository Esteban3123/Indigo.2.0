CREATE TABLE [dbo].[HCDOCOBVS] (
    [AUTO]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [OBSERDOCU] CHAR (100)   NULL,
    CONSTRAINT [PK_HCDOCOBVS] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCDOCOBVS_HCDOCUMAD] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCDOCUMAD] ([CONSECUTI])
);


GO
ALTER TABLE [dbo].[HCDOCOBVS] NOCHECK CONSTRAINT [FK_HCDOCOBVS_HCDOCUMAD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones generales del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'AUTO';

