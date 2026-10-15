CREATE TABLE [dbo].[INPACIENTTOPANU] (
    [Id]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ANIO]      INT                                                                              NOT NULL,
    [PAGADOCMO] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCOP] NUMERIC (18)                                                                     NOT NULL,
    [PAGADOCRE] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_INPACIENTTOPANU] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_INPACIENTTOPANU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INPACIENTTOPANU] NOCHECK CONSTRAINT [FK_INPACIENTTOPANU_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENTTOPANU_IPCODPACI_ANIO]
    ON [dbo].[INPACIENTTOPANU]([IPCODPACI] ASC, [ANIO] ASC)
    INCLUDE([PAGADOCMO], [PAGADOCOP], [PAGADOCRE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor pagado por cuota de recuperación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor pagado por regimen contributivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor pagado por cuota moderadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'PAGADOCMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el año en que se va a manejar el tope', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'ANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTTOPANU', @level2type = N'COLUMN', @level2name = N'Id';

