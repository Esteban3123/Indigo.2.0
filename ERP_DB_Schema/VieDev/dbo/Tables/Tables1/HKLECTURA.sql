CREATE TABLE [dbo].[HKLECTURA] (
    [tipo_doc]                 VARCHAR (2)                                                            NULL,
    [documento]                VARCHAR (20)                                                           NULL,
    [primer_nombre]            VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [segundo_nombre]           VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [primer_apellido]          VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [segundo_apellido]         VARCHAR (30) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)') NULL,
    [telefono]                 VARCHAR (50)                                                           NULL,
    [celular]                  VARCHAR (50)                                                           NULL,
    [direccion]                VARCHAR (256) MASKED WITH (FUNCTION = 'default()')                     NULL,
    [correo]                   VARCHAR (40)                                                           NULL,
    [sexo]                     VARCHAR (1)                                                            NULL,
    [rh_sanguineo]             VARCHAR (4)                                                            NULL,
    [Fecha_nacimiento]         DATE                                                                   NULL,
    [zona_residencial]         VARCHAR (1)                                                            NULL,
    [codigo_departamento]      VARCHAR (2)                                                            NULL,
    [nombre_departamento]      VARCHAR (30)                                                           NULL,
    [codigo_municipio]         VARCHAR (5)                                                            NULL,
    [nombre_municipio]         VARCHAR (30)                                                           NULL,
    [cups]                     VARCHAR (20)                                                           NULL,
    [nombre_cups]              VARCHAR (256)                                                          NULL,
    [modalidad]                VARCHAR (10)                                                           NULL,
    [codigo_interno_servicio]  INT                                                                    NULL,
    [centro_costo_solicitante] INT                                                                    NULL,
    [centro_costo_responde]    INT                                                                    NULL,
    [codigo_medico]            VARCHAR (50)                                                           NULL,
    [nombre_medico]            VARCHAR (120)                                                          NULL,
    [especialidad_solicitante] VARCHAR (150)                                                          NULL,
    [via_ingreso]              INT                                                                    NULL,
    [campo_urgencia]           CHAR (1)                                                               NULL,
    [codigo_cie_10]            VARCHAR (7)                                                            NULL,
    [descripción_cie_10]       VARCHAR (200)                                                          NULL,
    [nombre_eps]               VARCHAR (256)                                                          NULL,
    [nit_eps]                  VARCHAR (11)                                                           NULL,
    [fecha_solicitud]          DATE                                                                   NULL,
    [hora_solicitud]           TIME (7)                                                               NULL,
    [observacion]              VARCHAR (MAX)                                                          NULL,
    [justificacion]            VARCHAR (4000)                                                         NULL,
    [lectura]                  BIT                                                                    NULL,
    [prioridad]                VARCHAR (1)                                                            NULL,
    [estado_his]               VARCHAR (2)                                                            NULL,
    [cancelacion_his]          VARCHAR (1)                                                            NULL,
    [numero_solicitud]         INT                                                                    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [auto_imagen_his]          INT                                                                    NOT NULL,
    [url_imagen]               VARCHAR (256)                                                          NULL,
    CONSTRAINT [PK_HKLECTURA] PRIMARY KEY CLUSTERED ([numero_solicitud] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'cuarda   URL completa imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'auto_imagen_his';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador y llave primaria, código único de solictud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera por defecto 0  que nos informa si el HIS nos envia una cancelación marcando el campo con t  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cancelacion_his';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación de la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'justificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obsrevaciones de la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'observacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CIE10  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_cie_10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'N=no, S=si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'campo_urgencia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: 1=Ambulatorio, 2=Hospitalización, 3=Urgencias, 5=Cita prioritaria.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'via_ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: Medeicina general=1  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'especialidad_solicitante';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Quien genera la orden  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo_medico  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_medico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Área del hospital que responde la solicitud del procedimiento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_responde';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Área del hospital que hace la solicitud del procedimiento.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'centro_costo_solicitante';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código interno del servicio.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: RX, DX, MG, CT, RM, etc.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'modalidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del estudio, se cambio tipo de campo de text a varchar  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Códificacion única de procedimientos en salud.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: Medellín  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_municipio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: 001  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_municipio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: Antioquia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'nombre_departamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: 05  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'codigo_departamento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Urbana=U, Rural=R  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'zona_residencial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'YYYY-MM-DD  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'Fecha_nacimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ejemplo: O-, A-, AB+, AB-, A+, etc.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'rh_sanguineo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hombre=M, Mujer=F  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'sexo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'correo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'direccion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de celular  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'celular';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono fijo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'telefono';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellido del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellido del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'primer_apellido';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'segundo_nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del documento  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKLECTURA', @level2type = N'COLUMN', @level2name = N'documento';

