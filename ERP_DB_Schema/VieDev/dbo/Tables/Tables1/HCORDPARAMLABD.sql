CREATE TABLE [dbo].[HCORDPARAMLABD] (
    [ID]              INT                                                                        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORDPARAMLABID] INT                                                                        NOT NULL,
    [HCPARAMBID]      INT                                                                        NOT NULL,
    [RESULTADO]       VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)') NULL,
    [RESULTADOID]     INT                                                                        NULL,
    CONSTRAINT [PK_HCORDPARAMLABD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPARAMLABD_HCORDPARAMLAB] FOREIGN KEY ([HCORDPARAMLABID]) REFERENCES [dbo].[HCORDPARAMLAB] ([ID]),
    CONSTRAINT [FK_HCORDPARAMLABD_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDPARAMLABD] NOCHECK CONSTRAINT [FK_HCORDPARAMLABD_HCORDPARAMLAB];


GO
ALTER TABLE [dbo].[HCORDPARAMLABD] NOCHECK CONSTRAINT [FK_HCORDPARAMLABD_HCPARAMB];

