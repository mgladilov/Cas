using System;
using Controls.StatusImageLink;
using CAS.UI.Interfaces;
using Microsoft.VisualBasic.Devices;

namespace CAS.UI.Management.Dispatchering
{
    /// <summary>
    /// ������� ���������� StatusImageLinkLabel �������������� ������� �� ������
    /// </summary>
    public class ReferenceStatusImageLinkLabel : StatusImageLinkLabel,IReference
    {
        #region Fields

        #region private IDisplayer displayer
        /// <summary>
        /// Displayer for displaying entity
        /// </summary>
        private IDisplayer displayer;
        #endregion

        #region private DisplayingEntity entity
        /// <summary>
        /// Entity to display
        /// </summary>
        private IDisplayingEntity entity;
        #endregion

        #region private ReflectionTypes reflectionType
        private string displayerText;
        private ReflectionTypes reflectionType;
        #endregion

        #endregion

        #region Constructors

        #region public ReferenceStatusImageLinkLabel()

        /// <summary>
        /// Creates new instance of reference avbutton
        /// </summary>
        public ReferenceStatusImageLinkLabel()
        {
        }
        
        #endregion

        #region public ReferenceStatusImageLinkLabel(IDisplayingEntity entity, ReflectionTypes reflectionType)
        
        /// <summary>
        /// Creates new instance of reference avbutton
        /// </summary>
        /// <param name="entity">Entity to display</param>
        /// <param name="reflectionType">Type of displaying</param>
        public ReferenceStatusImageLinkLabel(IDisplayingEntity entity, ReflectionTypes reflectionType)
            : this(null, entity, reflectionType)
        {
        }
        
        #endregion

        #region public ReferenceStatusImageLinkLabel(IDisplayingEntity entity, ReflectionTypes reflectionType, string displayerText)

        /// <summary>
        /// Creates new instance of reference avbutton
        /// </summary>
        /// <param name="entity">Entity to display</param>
        /// <param name="reflectionType">Type of displaying</param>
        /// <param name="displayerText">Text to display</param>
        public ReferenceStatusImageLinkLabel(IDisplayingEntity entity, ReflectionTypes reflectionType, string displayerText)
            : this(null, entity, reflectionType, displayerText)
        {
        }

        #endregion

        #region public ReferenceStatusImageLinkLabel(IDisplayer displayer, IDisplayingEntity entity, ReflectionTypes reflectionType)
        
        /// <summary>
        /// Creates new instance of reference avbutton
        /// </summary>
        /// <param name="displayer">Displayer in which entity should be displayed</param>
        /// <param name="entity">Entity to display</param>
        /// <param name="reflectionType">Type of displaying</param>
        public ReferenceStatusImageLinkLabel(IDisplayer displayer, IDisplayingEntity entity, ReflectionTypes reflectionType) 
            : this(displayer, entity, reflectionType, "")
        {
        }
        
        #endregion

        #region public ReferenceStatusImageLinkLabel(IDisplayer displayer, IDisplayingEntity entity, ReflectionTypes reflectionType, string displayerText)

        /// <summary>
        /// Creates new instance of reference avbutton
        /// </summary>
        /// <param name="displayer">Displayer in which entity should be displayed</param>
        /// <param name="entity">Entity to display</param>
        /// <param name="reflectionType">Type of displaying</param>
        /// <param name="displayerText">Text to display</param>
        public ReferenceStatusImageLinkLabel(IDisplayer displayer, IDisplayingEntity entity, ReflectionTypes reflectionType, string displayerText)
        {
            this.displayer = displayer;
            this.entity = entity;
            this.reflectionType = reflectionType;
            this.displayerText = displayerText;
        }

        #endregion


        #endregion

        #region Methods

        #region protected void OnDisplayerRequested()

        /// <summary>
        /// ����� ��������� ������� DisplayerRequested
        /// </summary>
        protected void OnDisplayerRequested()
        {
            if (null != DisplayerRequested)
            {
                ReflectionTypes reflection = reflectionType;
                Keyboard k = new Keyboard();
                if (k.ShiftKeyDown && reflection == ReflectionTypes.DisplayInCurrent) reflection = ReflectionTypes.DisplayInNew;
                if (null != displayer)
                {
                    DisplayerRequested(this, new ReferenceEventArgs(entity, reflection, displayer, displayerText));
                }
                else
                {
                    DisplayerRequested(this, new ReferenceEventArgs(entity, reflection, displayerText));
                }
            }
        }

        #endregion

        #region protected override void OnClick(EventArgs e)

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.Click"></see> event.
        ///</summary>
        ///
        ///<param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data. </param>
        protected override void OnClick(EventArgs e)
        {
            OnDisplayerRequested();
            base.OnClick(e);
        }

        #endregion

        #region public void DisplayRequested()
        public void DisplayRequested()
        {
            OnDisplayerRequested();
        }
        #endregion

        #endregion

        #region Properties

        #region public IDisplayer Displayer

        /// <summary>
        /// Displayer for displaying entity
        /// </summary>
        public IDisplayer Displayer
        {
            get { return displayer; }
            set { displayer = value; }
        }

        #endregion

        #region public string DisplayerText

        /// <summary>
        /// Text of page's header that Reference lead to
        /// </summary>
        public string DisplayerText
        {
            get { return displayerText; }
            set { displayerText = value; }
        }

        #endregion

        #region public IDisplayingEntity Entity

        /// <summary>
        /// Entity to display
        /// </summary>
        public IDisplayingEntity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        #endregion

        #region public ReflectionTypes ReflectionType

        /// <summary>
        /// Type of reflection [:|||:]
        /// </summary>
        public ReflectionTypes ReflectionType
        {
            get { return reflectionType; }
            set { reflectionType = value; }
        }

        #endregion

        #endregion

        #region Event

        #region public event EventHandler<ReferenceEventArgs> DisplayerRequested
        /// <summary>
        /// Occurs when linked invoker requests displaying 
        /// </summary>
        public event EventHandler<ReferenceEventArgs> DisplayerRequested;
        #endregion

        #endregion

    }
}
