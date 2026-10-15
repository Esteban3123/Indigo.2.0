CREATE TABLE [dbo].[ADPOBESPEPAC] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [IDADPOBESPE] INT                                                                              NULL,
    [ESTADO]      BIT                                                                              CONSTRAINT [DF_ADPOBESPEPAC_ESTADO] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_ADPOBESPEPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADPOBESPEPAC_ADPOBESPE] FOREIGN KEY ([IDADPOBESPE]) REFERENCES [dbo].[ADPOBESPE] ([ID]),
    CONSTRAINT [FK_ADPOBESPEPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_ADPOBESPE];


GO
ALTER TABLE [dbo].[ADPOBESPEPAC] NOCHECK CONSTRAINT [FK_ADPOBESPEPAC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado    Me identifica si el paciente aun contiene la poblaicón especial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPOBESPEPAC', @level2type = N'COLUMN', @level2name = N'ESTADO';

