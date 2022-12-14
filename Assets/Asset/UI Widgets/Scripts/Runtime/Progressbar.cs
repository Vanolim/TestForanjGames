using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Unitycoding.UIWidgets{
	public class Progressbar : UIWidget {
		[Header("Reference")]
		[SerializeField]
		protected Image progressbar;
		[SerializeField]
		protected Text progressLabel;
		[SerializeField]
		protected string format="F0";

		protected virtual void Start(){
			progressbar.type=Image.Type.Filled;
		}

		public virtual void SetProgress(float progress){
			progressbar.fillAmount = progress;
			if (progressLabel != null) {
				progressLabel.text=(progress*100f).ToString(format)+"%";
			}
		}
		
		public override void Show ()
		{
			progressbar.fillAmount = 0f;
			if (progressLabel != null) {
				progressLabel.text="0%";
			}
			base.Show ();
		}
	}
}