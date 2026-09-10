'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Juan F. Tamayo
' Created          : 2013-07-23
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-23
' Description      : Clase parcial y de servicios para la entidad GlosaObjectionsReceptionC
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Object

''' <summary>
''' Clase de servicios para la entidad <see cref="Domain.Entities.GlosaObjectionsReceptionC" />
''' </summary>
Public Class GlosaObjectionsReceptionCService

    ''' <summary>
    ''' Refresca las propiedades StateRecord de todos las entidades de la lista en el parametro
    ''' <paramref name="listGlosaObjectionReceptionC" />
    ''' </summary>
    ''' <param name="listGlosaObjectionReceptionC">Lista de entidades <see cref="Domain.Entities.GlosaObjectionsReceptionC" /></param>
    ''' <param name="process">Proceso que indica la lógica a tener en cuenta para refrescar el estado de las entidades</param>
    ''' <returns>La lista de entidades con la propiedad StateRecord actualizada segun el proceso</returns>
    Public Shared Function RefreshStateRecordProperties(ByVal listGlosaObjectionReceptionC As List(Of Domain.Entities.GlosaObjectionsReceptionC), ByVal process As Domain.Entities.GlosaObjectionsReceptionC.EProcess) As List(Of Domain.Entities.GlosaObjectionsReceptionC)
        'For Each o As Domain.Entities.GlosaObjectionsReceptionC In listGlosaObjectionReceptionC
        '    o.RefreshStateRecordProperty(process)
        'Next
        Return listGlosaObjectionReceptionC
    End Function

End Class

''' <summary>
''' Clase parcial de la entidad <see cref="Domain.Entities.GlosaObjectionsReceptionC" />
''' </summary>
Partial Public Class GlosaObjectionsReceptionC

#Region "Enums"

    ''' <summary>
    ''' Enumera los procesos del módulo de glosas en lo que se puede
    ''' llamar a refrescar la propiedad StateRecord de la entidad
    ''' </summary>
    Public Enum EProcess
        ''' <summary>
        ''' Proceso de coordinación
        ''' </summary>
        Coordination
    End Enum

#End Region

#Region "Methods"

    ''' <summary>
    ''' Refresca la propiedad StateRecord de la entidad aplicando la lógica necesaria
    ''' para el procesos indicado en el parámetro <paramref name="process" />
    ''' </summary>
    ''' <param name="process">Proceso que indica la lógica a aplicar en la actualización de la propiedad</param>
    Public Sub RefreshStateRecordProperty(ByVal process As EProcess)
        Me.StateRecord = True
        Select Case process
            Case EProcess.Coordination
                If Me.GlosaObjectionsReceptionD IsNot Nothing Then
                    For Each d As Domain.Entities.GlosaObjectionsReceptionD In Me.GlosaObjectionsReceptionD
                        If d.GlosaInvoiceDetail IsNot Nothing Then
                            For Each inv As Domain.Entities.GlosaInvoiceDetail In d.GlosaInvoiceDetail
                                If (From m As Domain.Entities.GlosaMovementGlosa In inv.GlosaMovementGlosa Where m.MainGlosa = True And (m.State = 1 Or m.State = 3) Select m).Any Then
                                    Me.StateRecord = False
                                    Exit Sub
                                End If
                            Next
                        End If
                    Next
                End If
        End Select
    End Sub

    Function clone() As GlosaObjectionsReceptionC
        Return Me.MemberwiseClone()
    End Function

#End Region

#Region "Manual Properties"

    Private _StateRecord As Boolean
    Property StateRecord As Boolean
        Get
            Return _StateRecord
        End Get
        Set(value As Boolean)
            _StateRecord = value
        End Set
    End Property

    Private _NitToPersist As String
    Property NitToPersist As String
        Get
            Return _NitToPersist
        End Get
        Set(value As String)
            _NitToPersist = value
        End Set
    End Property

#End Region

End Class