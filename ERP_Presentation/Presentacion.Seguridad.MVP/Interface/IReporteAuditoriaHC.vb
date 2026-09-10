'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Johan Sebastian Carranza Ramos 
' Created          : 17-05-2019
'
' Last Modified By : Johan Sebastian Carranza Ramos
' Last Modified On : 17-05-2019
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libererias Importadas"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Security.Entities

#End Region


''' <summary>
''' Esta interfaz contiene los campos de nuestra vista y los que implementa nuestro presentador
''' </summary>
''' <remarks></remarks>
Public Interface IReporteAuditoriaHC
    Inherits IcrudBase


#Region "Metodos"

    ''' <summary>
    ''' Metodo que sirve para guardar los cambios realizados en la contraseña
    ''' </summary>
    Sub Guardar()
    ''' <summary>
    ''' Metodo que Deshace todas las operaciones realizadas y Limpia los controles utilizados
    ''' </summary>
    Sub Deshacer()
    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Sub LogicaBotonActualizar(ByVal existeDatos As Boolean)

    Sub AsyncLoader(ByVal valor As Boolean)
#End Region


#Region "Propiedades"
    ''' <summary>
    ''' esta propiedad contiene el codigo del paciente
    ''' </summary>
    Property INDPaciente As String

    ''' <summary>
    ''' propiedad que obtiene o establece el usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property User As User

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InitialDate As Date?

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia Final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FinalDate As Date?

#End Region


End Interface
