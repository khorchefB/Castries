'use client';
import React from 'react'
import Countdown, { zeroPad } from 'react-countdown';

const renderer = ({ days, hours, minutes, seconds, completed }:{ days: number,  hours:number, minutes:number, seconds:number, completed:boolean  } ) => {
    return(
        <div className={`border-2 border-whiite text-white py-1 px-2 rounded-lg flex justify-center 
                        ${completed ? 'bg-green-500':(days === 0 && hours < 10 ) ? 'bg-amber-600': 'bg-green-600'}`}>
            {completed ? (
                <span>Finished</span>
            ) : (
                <span>
                    {days}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
                </span>
            )}
        </div>
    )
};
 
type Props = {
    auctionEnd: string;
}

export default function CountDownTimer({auctionEnd}: Props) {
  return (
    <div>
    <Countdown date={auctionEnd} renderer={renderer} />
    </div>
  )
}
