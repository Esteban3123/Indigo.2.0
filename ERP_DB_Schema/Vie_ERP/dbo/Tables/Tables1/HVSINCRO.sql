CREATE TABLE [dbo].[HVSINCRO] (
    [ID]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPREGUNTA] INT                                                                              NULL,
    [CORREO]     VARCHAR (200) MASKED WITH (FUNCTION = 'email()')                                 NULL,
    [RESPUESTA]  VARCHAR (500)                                                                    NULL,
    CONSTRAINT [PK_HVSINCRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HVSINCRO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HVSINCRO] NOCHECK CONSTRAINT [FK_HVSINCRO_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'RESPUESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo del paciente Encriptado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preguntas de seguridad predefinidas:  1. "Nombre de su primera mascota",  2. "Nombre de su abuelo paterno",  3. "Apodo de la infancia",  4. "Color favorito",  5. "Colegio al cual asistio en la primaria",  6. "Equipo de futbol al cual anima",  7. "Ciudad de nacimiento de su padre",  8. "Nombre de su abuela materna",  9. "Colegio al cual asistio en la secundaria",  10. "Ciudad de nacimiento de su madre"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IDPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente que se sincroniza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Sincronizaciones Health Vault', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'ID';

