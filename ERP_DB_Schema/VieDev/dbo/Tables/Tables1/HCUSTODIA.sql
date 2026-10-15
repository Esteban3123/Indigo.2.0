CREATE TABLE [dbo].[HCUSTODIA] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CUSTODIA]      BIT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODUSUCUST]    VARCHAR (15)                                                                     NULL,
    [FECHACUSTO]    DATETIME                                                                         NULL,
    [CODUSUDESCU]   VARCHAR (15)                                                                     NULL,
    [FECHADESCUSTO] DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCUSTODIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCUSTODIA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCUSTODIA] NOCHECK CONSTRAINT [FK_HCUSTODIA_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True -> Custodiado  False -> No en custodia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUSTODIA', @level2type = N'COLUMN', @level2name = N'CUSTODIA';

