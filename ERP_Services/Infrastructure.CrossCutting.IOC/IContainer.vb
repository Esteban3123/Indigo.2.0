'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.IOC
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Interface para el Contenedor IOC
''' </summary>
Public Interface IContainer

    ''' <summary>
    ''' Resuelve la dependencia TService
    ''' </summary>
    ''' <typeparam name="TService">Tipo de la Dependencia</typeparam>
    ''' <returns>Instancia de TService</returns>
    Function Resolve(Of TService)() As TService

    ''' <summary>
    ''' Resuelve el Tipo del Constructor y retorna el objeto como una instancia de TService
    ''' </summary>
    ''' <returns>instance of this type</returns>
    Function Resolve(ByVal type As Type) As Object

    ''' <summary>
    ''' Registra un Tipo en el Localizador
    ''' </summary>
    ''' <param name="type">Tipo a registrar</param>
    Sub RegisterType(ByVal type As Type)

End Interface