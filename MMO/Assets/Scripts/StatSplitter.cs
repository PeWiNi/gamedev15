using UnityEngine;
using System.Collections;

public class StatSplitter : MonoBehaviour
{

	/* TODO:
	 * Get pseudo-algorithm for stat splitting and implement it.
	 * 
	 * */
	// High Hp = Low TailSlap
	// High BoomNana = Low AOEDamage

	/*
     Hp1= Hmax/noP+Rand(-20% * Hmax/noP, 20%*Hmax/noP)
     Hplast=Hmax-(Hp1+Hp2...+HnoP-1 = ΣHnop-1)
     */
	int maximumHp = 1000;
	int maximumBoom = 150;
	int maximumTail = 75;
	int maximumAOE = 20;
	public float scaleFactor = 3;
	public ArrayList hpValues = new ArrayList ();
	public ArrayList boomValues = new ArrayList ();
	public ArrayList tailValues = new ArrayList ();
	public ArrayList aoeValues = new ArrayList ();

	public void splitHp (int noP)
	{
		hpValues = new ArrayList ();
		float sum = 0;
		for (int i = 0; i < noP-1; i++) {
			float hp = (maximumHp / noP + Random.Range ((float)0.8 * (maximumHp / noP), (float)1.2 * (maximumHp / noP)));
			sum += hp;
			hpValues.Add (hp);
		}
		float hpLast = maximumHp - (sum);
		hpValues.Add (hpLast);
	}


	/*
     redoCalc=true;

 while (redoCalc))

 { for each BMnoP without the last one

 BM1=Rand(Rand (0,BDMGmax/noP), BDMGmax/2)

 if ( BM1 < 20% *BDMGmax) then BM1=0;

 for the BMlast = BDMGmax-ΣBMnoP-1

 if ( BMlast < 20% *BDMGmax) 

 then redoCalc=true; }
     */
	public void splitBoom (int noP)
	{
		bool redoCalc = true;
		while (redoCalc) {
			boomValues = new ArrayList ();
			float sum = 0;
			for (int i = 0; i < noP - 1; i++) {
				float bm1 = Random.Range (Random.Range (0, maximumBoom / noP), maximumBoom / 2);
				if (bm1 < (0.8 * maximumBoom)) {
					bm1 = 0;
				}
				sum += bm1;
				boomValues.Add (bm1);
			}
			float bmLast = maximumBoom - sum;
			if (bmLast < (0.8 * maximumBoom)) {
				redoCalc = true;
			} else {
				redoCalc = false;
			}
			boomValues.Add (bmLast);
		}
	}
	/*
     Same Calculation as Boomnana damage
     */
	public void splitTail (int noP)
	{
		bool redoCalc = true;
		while (redoCalc) {
			tailValues = new ArrayList ();
			float sum = 0;
			for (int i = 0; i < noP - 1; i++) {
				float ts1 = Random.Range (Random.Range (0, maximumTail / noP), maximumTail / 2);
				if (ts1 < (0.8 * maximumTail)) {
					ts1 = 0;
				}
				sum += ts1;
				tailValues.Add (ts1);
			}
			float tsLast = maximumTail - sum;
			if (tsLast < (0.8 * maximumTail)) {
				redoCalc = true;
			} else {
				redoCalc = false;
			}
			tailValues.Add (tsLast);
		}
	}

	public void splitScale (int noP)
	{
		float scaleMax = 3;
		if (noP < 3) {
			// split normally
			scaleFactor = scaleMax / noP;
		} else if (noP > 3 && noP < 10) {
			// split /10 + 1
			scaleFactor = (scaleMax / noP) + 1;
		} else {
			//scale = 1;
			scaleFactor = 1;
		}
	}

	public void splitAoe (int noP)
	{
		bool redoCalc = true;
		while (redoCalc) {
			aoeValues = new ArrayList ();
			float sum = 0;
			for (int i = 0; i < noP - 1; i++) {
				float aoe1 = Random.Range (Random.Range (0, (float)(maximumAOE / noP)), (float)(1.3 * maximumAOE));
				if (aoe1 < (0.7 * maximumAOE)) {
					aoe1 = 0;
				}
				sum += aoe1;
				aoeValues.Add (aoe1);
			}
			float aoeLast = maximumAOE - sum;
			if (aoeLast < (0.7 * maximumAOE)) {
				redoCalc = true;
			} else {
				redoCalc = false;
			}
			aoeValues.Add (aoeLast);
		}
	}

	public void splitStats (int noP)
	{
		splitHp (noP);
		splitBoom (noP);
		splitTail (noP);
		splitAoe (noP);
	}


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
