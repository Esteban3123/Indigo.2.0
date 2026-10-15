CREATE TABLE [dbo].[ODOPERDIE] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL]   INT           NOT NULL,
    [NUMDIENTE]   TINYINT       NOT NULL,
    [MOVILIDAD]   CHAR (1)      NULL,
    [NOTA]        VARCHAR (MAX) NULL,
    [IMPLANTE]    BIT           NULL,
    [DIENAUSENTE] BIT           NULL,
    CONSTRAINT [PK_ODOPERDIE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPERDIE_ODOPERICTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODOPERCTR] ([ID])
);


GO
ALTER TABLE [dbo].[ODOPERDIE] NOCHECK CONSTRAINT [FK_ODOPERDIE_ODOPERICTR];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Movilidad    1:  I  2:  II  3:  III', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';

