Imports DevExpress.Mvvm

Namespace Example.ViewModel
    Public Class MainViewModel
        Inherits ViewModelBase

#Disable Warning BC42104 ' Variable is used before it has been assigned a value
        Public Property FirstName As String
            Get
                Return GetProperty(Function() FirstName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() FirstName, value, AddressOf UpdateFullName)
            End Set
        End Property
        Public Property LastName As String
            Get
                Return GetProperty(Function() LastName)
            End Get
            Set(ByVal value As String)
                SetProperty(Function() LastName, value, AddressOf UpdateFullName)
            End Set
        End Property
        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
#Enable Warning BC42104 ' Variable is used before it has been assigned a value

        Private Sub UpdateFullName()
            RaisePropertyChanged(Function() FullName)
        End Sub
        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            FirstName = "FirstName in DesignMode"
            LastName = "LastName in DesignMode"
        End Sub
        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            FirstName = "FirstName"
            LastName = "LastName"
        End Sub

        Private ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return GetService(Of IMessageBoxService)()
            End Get
        End Property
        Private privateShowMessageCommand As DelegateCommand(Of String)
        Public Property ShowMessageCommand() As DelegateCommand(Of String)
            Get
                Return privateShowMessageCommand
            End Get
            Private Set(ByVal value As DelegateCommand(Of String))
                privateShowMessageCommand = value
            End Set
        End Property
        Private Sub ShowMessage(ByVal message As String)
            MessageBoxService.Show(message)
        End Sub
        Private Function CanShowMessage(ByVal message As String) As Boolean
            Return Not String.IsNullOrEmpty(message)
        End Function
        Public Sub New()
            ShowMessageCommand = New DelegateCommand(Of String)(AddressOf ShowMessage, AddressOf CanShowMessage)
        End Sub
    End Class
End Namespace
