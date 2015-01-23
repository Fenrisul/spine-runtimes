﻿/******************************************************************************
 * Spine Runtimes Software License
 * Version 2.1
 * 
 * Copyright (c) 2013, Esoteric Software
 * All rights reserved.
 * 
 * You are granted a perpetual, non-exclusive, non-sublicensable and
 * non-transferable license to install, execute and perform the Spine Runtimes
 * Software (the "Software") solely for internal use. Without the written
 * permission of Esoteric Software (typically granted by licensing Spine), you
 * may not (a) modify, translate, adapt or otherwise create derivative works,
 * improvements of the Software or develop new applications using the Software
 * or (b) remove, delete, alter or obscure any trademarks or any copyright,
 * trademark, patent or other intellectual property or proprietary rights
 * notices on or in the Software, including any copy thereof. Redistributions
 * in binary or source form must include this license and terms.
 * 
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

/*****************************************************************************
 * FootSoldierExample created by Mitch Thompson
 * Full irrevocable rights and permissions granted to Esoteric Software
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class FootSoldierExample : MonoBehaviour {
	[SpineAnimation("Idle")]
	public string idleAnimation;

	[SpineAnimation]
	public string attackAnimation;

	[SpineSlot]
	public string eyesSlot;

	[SpineAttachment(currentSkinOnly: true, slotField: "eyesSlot")]
	public string eyesOpenAttachment;

	[SpineAttachment(currentSkinOnly: true, slotField: "eyesSlot")]
	public string blinkAttachment;

	[Range(0, 0.2f)]
	public float blinkDuration = 0.05f;

	private SkeletonAnimation skeletonAnimation;

	void Awake() {
		skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	void Start() {
		skeletonAnimation.state.SetAnimation(0, idleAnimation, true);
		StartCoroutine("Blink");
	}

	void Update() {
		if (Input.GetKey(KeyCode.Space)) {
			if (skeletonAnimation.state.GetCurrent(0).Animation.Name != attackAnimation) {
				skeletonAnimation.state.SetAnimation(0, attackAnimation, false);
				skeletonAnimation.state.AddAnimation(0, idleAnimation, true, 0);
			}
		}
	}

	IEnumerator Blink() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(0.25f, 3f));
			skeletonAnimation.skeleton.SetAttachment(eyesSlot, blinkAttachment);
			yield return new WaitForSeconds(blinkDuration);
			skeletonAnimation.skeleton.SetAttachment(eyesSlot, eyesOpenAttachment);
		}
	}
}