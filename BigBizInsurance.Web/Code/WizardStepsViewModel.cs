using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBizInsurance.Web.Code
{
    /// <summary>
    /// Class WizardStepsViewModel.
    /// </summary>
    public class WizardStepsViewModel
    {
        /// <summary>
        /// Gets or sets the step number.
        /// </summary>
        /// <value>The step number.</value>
        public int StepNumber { set; get; }
        /// <summary>
        /// Gets or sets the step text.
        /// </summary>
        /// <value>The step text.</value>
        public string StepText { set; get; }

        /// <summary>
        /// Gets or sets the wizard step status.
        /// </summary>
        /// <value>The wizard step status.</value>
        public WizardStepStatusEnum WizardStepStatus { set; get; }
        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName
        {
            get
            {
                switch (WizardStepStatus)
                {
                    case WizardStepStatusEnum.Previous:
                        return "selected";
                    case WizardStepStatusEnum.Current:
                        return "selected";
                    case WizardStepStatusEnum.Next:
                        return "disabled";
                    default:
                        return "";
                }
            }
        }
    }

    /// <summary>
    /// Enum WizardStepStatusEnum
    /// </summary>
    public enum WizardStepStatusEnum
    {
        /// <summary>
        /// The previous
        /// </summary>
        Previous,
        /// <summary>
        /// The current
        /// </summary>
        Current,
        /// <summary>
        /// The next
        /// </summary>
        Next
    }
}
